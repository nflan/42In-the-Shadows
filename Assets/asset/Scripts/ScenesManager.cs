using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] private GameObject m_Menu;
    [SerializeField] private GameObject m_GameMenu;
    [SerializeField] private GameObject m_MenuPanel;
    [SerializeField] private TutorialScript m_TutorialScript;
    [SerializeField] private SettingsMenu m_SettingsMenu;
    [SerializeField] private GameObject m_SettingsMenuObj;
    [SerializeField] private GameObject m_ControlsPanel;
    [SerializeField] private StageSelection m_StageSelection;
    [SerializeField] private bool m_isMainMenu = false;
    [SerializeField] private string m_NextScene;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        this.PrintPlayerPrefs();
        if (m_GameMenu == null || m_SettingsMenu == null || m_ControlsPanel == null || m_MenuPanel == null || m_StageSelection == null)
        {
            if (m_MenuPanel == null)
            {
                m_MenuPanel = this.transform.Find("Menu").Find("MenuPanel").gameObject;
            }
            foreach (Transform child in this.m_MenuPanel.transform)
            {
                if (m_GameMenu == null && child.name == "GameMenu")
                {
                    m_GameMenu = child.gameObject;
                }
                if (m_SettingsMenu == null && child.name == "SettingsMenu")
                {
                    m_SettingsMenuObj = child.gameObject;
                    m_SettingsMenu = m_SettingsMenuObj.GetComponent<SettingsMenu>();
                }
            }
            if (m_StageSelection == null && this.transform.Find("StageSelection"))
            {
                m_StageSelection = this.transform.Find("StageSelection").GetComponent<StageSelection>();
            }
            if (m_ControlsPanel == null)
            {
                m_ControlsPanel = this.transform.Find("Menu").Find("ControlsPanel").gameObject;
            }

        }
        this.ApplyPlayerSettings();
        if (m_isMainMenu)
        {
            if (PlayerPrefs.GetInt("LoadStageSelection", 0) == 1)
            {
                PlayerPrefs.SetInt("LoadStageSelection", 0);
                this.LoadStageSelection();
            }
            else
            {
                this.DisplayMainMenu();
            }
        }
    }

    private void PrintPlayerPrefs()
    {
        Debug.Log("PlayerPrefs:");
        Debug.Log("ActiveSave: " + PlayerPrefs.GetInt("ActiveSave"));
        Debug.Log("Stage1: " + PlayerPrefs.GetFloat("Stage1"));
        Debug.Log("Stage2: " + PlayerPrefs.GetFloat("Stage2"));
        Debug.Log("Stage3: " + PlayerPrefs.GetFloat("Stage3"));
        Debug.Log("CurrentLevel: " + PlayerPrefs.GetFloat("CurrentLevel"));
        Debug.Log("FullScreen: " + PlayerPrefs.GetInt("FullScreen"));
        Debug.Log("Resolution: " + PlayerPrefs.GetInt("Resolution"));
        Debug.Log("Quality: " + PlayerPrefs.GetInt("Quality"));
        Debug.Log("Music: " + PlayerPrefs.GetInt("Music"));
        Debug.Log("Volume: " + PlayerPrefs.GetFloat("Volume"));
        Debug.Log("UpdateSelectionStage: " + PlayerPrefs.GetFloat("UpdateSelectionStage"));
        Debug.Log("Savane: " + PlayerPrefs.GetFloat("Savane"));
    }

    public void ResetSave()
    {
        PlayerPrefs.SetFloat("Stage1", 1.1f);
        PlayerPrefs.SetInt("ActiveSave", 1);
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("Stage2");
        PlayerPrefs.DeleteKey("Stage3");
        this.m_StageSelection.ResetSave();
    }

    public void ApplyPlayerSettings()
    {
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            this.m_SettingsMenu.SetFullScreen(PlayerPrefs.GetInt("FullScreen") == 1);
        }
        else
        {
            Debug.LogWarning("No FullScreen key found, setting to true");
            this.m_SettingsMenu.SetFullScreen(true);
        }
        if (PlayerPrefs.HasKey("Resolution"))
        {
            this.m_SettingsMenu.SetResolution(PlayerPrefs.GetInt("Resolution"));
        }
        else
        {
            this.m_SettingsMenu.SetResolution(Screen.resolutions.Length - 1);
        }
        if (PlayerPrefs.HasKey("Quality"))
        {
            this.m_SettingsMenu.SetQuality(PlayerPrefs.GetInt("Quality"));
        }
        else
        {
            this.m_SettingsMenu.SetQuality(QualitySettings.count - 1);
        }
        if (PlayerPrefs.HasKey("Music"))
        {
            this.m_SettingsMenu.ToggleMusic(PlayerPrefs.GetInt("Music") == 1);
        }
        else
        {
            this.m_SettingsMenu.ToggleMusic(true);
        }
        if (PlayerPrefs.HasKey("Volume"))
        {
            this.m_SettingsMenu.SetVolume(PlayerPrefs.GetFloat("Volume"));
        }
        else
        {
            this.m_SettingsMenu.SetVolume(50f);
        }
    }

    public void SetTestMode(bool test)
    {
        PlayerPrefs.SetInt("TestMode", test ? 1 : 0);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        this.ApplyPlayerSettings();
    }

    public void SetNextScene(string scene)
    {
        m_NextScene = scene;
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UnloadScene(int scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    IEnumerator LoadAsyncScene(string scene)
    {
        Scene active = SceneManager.GetActiveScene();
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        SceneManager.UnloadSceneAsync(active);
    }

    public void GoToNextScene()
    {
        // Use a coroutine to load the Scene in the background
        StartCoroutine(LoadAsyncScene(this.m_NextScene));
    }

    public void GoToScene(string scene)
    {
        // Use a coroutine to load the Scene in the background
        StartCoroutine(LoadAsyncScene(scene));
    }

    public void LoadStageSelection()
    {
        m_GameMenu.gameObject.SetActive(false);
        m_StageSelection.gameObject.SetActive(true);
    }

    public void DisplaySettingsMenu()
    {
        this.HideControlsPanel();
        m_GameMenu.SetActive(false);
        m_Menu.SetActive(true);
        m_MenuPanel.SetActive(true);
        m_SettingsMenu.ActiveMenu(true);
    }
    public void HideSettingsMenu()
    {
        m_SettingsMenu.ActiveMenu(false);
    }

    public void DisplayControlsMenu()
    {
        this.HideGamePanel();
        m_ControlsPanel.SetActive(true);
    }
    public void HideControlsPanel()
    {
        if (m_ControlsPanel.activeSelf)
        {
            m_ControlsPanel.SetActive(false);
        }
    }

    IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float durationFade)
    {
        float elapsedTime = 0f;
        while (elapsedTime < durationFade)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / durationFade);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, startAlpha > 0.5f ? 1 : 0, t);
            yield return null;
        }
    }

    public void HideStageSelection()
    {
        if (m_StageSelection && m_StageSelection.gameObject.activeSelf)
        {
            m_StageSelection.gameObject.SetActive(false);
        }
    }

    public void ShowTutorial(int tuto)
    {
        this.HideSettingsMenu();
        this.HideControlsPanel();
        m_GameMenu.SetActive(false);
        m_Menu.SetActive(true);
        m_MenuPanel.SetActive(true);
        m_TutorialScript.ActiveMenu(tuto);
    }

    public void HideTutorial()
    {
        m_TutorialScript.DesactiveMenu();
    }

    public void ResetMainMenu()
    {
        this.HideSettingsMenu();
        this.HideControlsPanel();
        this.DisplayMainMenu();
    }
    public void DisplayMainMenu()
    {
        this.HideStageSelection();
        m_Menu.SetActive(true);
        m_MenuPanel.SetActive(true);
        m_SettingsMenuObj.SetActive(false);
        m_GameMenu.SetActive(true);
        m_ControlsPanel.SetActive(false);
    }

    public void SetLoadStageSelection()
    {
        PlayerPrefs.SetInt("LoadStageSelection", 1);
    }

    public void HideGameMenu()
    {
        this.HideGamePanel();
        this.HideControlsPanel();
        this.HideSettingsMenu();
        this.HideTutorial();
    }
    public void HideGamePanel()
    {
        m_MenuPanel.SetActive(false);
        m_GameMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Tools.QuitGame();
    }
}
