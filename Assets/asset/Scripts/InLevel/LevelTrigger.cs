using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private List<GameObject> m_LevelsObjs = new List<GameObject>();

    [Header("Game Info")]
    [SerializeField] private List<AudioSource> m_AudioSources = new List<AudioSource>();
    [SerializeField] private ScenesManager m_ScenesManager;
    private InputController m_InputController;
    private GameTimer m_Timer;
    private SettingsMenu m_SettingsMenu;

    void Start()
    {
        if (m_LevelsObjs.Count == 0)
        {
            Tools.QuitGame();
        }
        m_InputController = Tools.InitTypes(m_InputController, this.name);
        m_Timer = Tools.InitTypes(m_Timer, this.name);
        m_SettingsMenu = Tools.InitTypes(m_SettingsMenu, this.name);
        this.DisableAllLevels();
        this.ActivateLevel();
    }
    
    private void DisableAllLevels()
    {
        foreach (GameObject level in m_LevelsObjs)
        {
            level.SetActive(false);
        }
    }

    private void ActivateLevel()
    {
        float currentLevel = PlayerPrefs.GetFloat("CurrentLevel", 0f);
        if (currentLevel < 0.0000001f)
        {
            Debug.LogError("No level selected");
            Tools.QuitGame();
        }
        foreach (GameObject level in m_LevelsObjs)
        {
            if (level.name == "Level" + Mathf.FloorToInt(currentLevel) || level.name == "Level" + currentLevel)
            {
                if (m_SettingsMenu != null && m_AudioSources.Count > 0 && level.name == "Level" + Mathf.FloorToInt(currentLevel))
                    m_SettingsMenu.SetAudioSource(this.m_AudioSources[Mathf.FloorToInt(currentLevel) - 1]);
                level.SetActive(true);
            }
        }
        m_InputController.gameObject.SetActive(true);
        m_Timer.gameObject.SetActive(true);
        if (Mathf.RoundToInt(currentLevel * 10) % 10 == 1)
        {
            m_InputController.PauseTutorial(Mathf.FloorToInt(currentLevel));
        }
    }
}
