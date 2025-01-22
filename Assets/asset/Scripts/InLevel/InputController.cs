using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] public bool m_IsPaused = false;
    [SerializeField] private List<ObjectRotation> m_ObjectRotations;    
    [SerializeField] private ScenesManager m_ScenesManager;
    [SerializeField] private LevelInformations m_LevelInformations;
    private int m_Object = 0;
    private int m_ObjectCount = 0;
    private PlayerInput m_PlayerInput;
    private InputAction m_PauseAction;
    private InputAction m_ResetPos;
    private int m_CurrentLevel;

    float m_TurnX;
    float m_TurnY;

    void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_LevelInformations = GameObject.FindObjectOfType<LevelInformations>();
        if (!m_PlayerInput || !m_LevelInformations)
        {
            Debug.LogError("PlayerInput or LevelInformations not found");
            Tools.QuitGame();
        }
        m_PauseAction = m_PlayerInput.actions["Menu"];
        m_ResetPos = m_PlayerInput.actions["ResetPos"];
        if (m_ObjectRotations.Count == 0)
        {
            foreach (ObjectRotation obj in GameObject.FindObjectsOfType<ObjectRotation>())
            {
                m_ObjectRotations.Add(obj);
            }
            m_ObjectRotations.Sort((x, y) => x.name.CompareTo(y.name));
            m_ObjectCount = m_ObjectRotations.Count;
            if (m_ObjectCount == 0)
            {
                Debug.LogError("Object Rotation not found");
                Tools.QuitGame();
            }
        }
        m_CurrentLevel = Mathf.FloorToInt(PlayerPrefs.GetFloat("CurrentLevel", -1));
    }

    void OnEnable()
    {
        m_PauseAction.Enable();
        m_ResetPos.Enable();
    }

    void Start()
    {
        m_PauseAction.performed += _ => PauseGame();
        m_ResetPos.performed += _ => m_ObjectRotations[m_Object].RestartPos();
    }

    private bool IsFinish()
    {
        foreach (ObjectRotation obj in m_ObjectRotations)
        {
            if (!obj.GetFinish())
            {
                return false;
            }
        }
        return true;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsFinish())
        {
            m_LevelInformations.EndStage();
            Time.timeScale = 0f;
            this.gameObject.SetActive(false);
        }
        if (m_IsPaused)
        {
            return;
        }
        if (m_PlayerInput.actions["Hint"].triggered)
        {
            this.m_LevelInformations.Hint();
        }
        if (m_PlayerInput.actions["Rotate"].IsPressed())
        {
            m_TurnX = m_PlayerInput.actions["Rotation"].ReadValue<Vector2>().x;
            m_TurnY = m_PlayerInput.actions["Rotation"].ReadValue<Vector2>().y;

            if (m_CurrentLevel > 2 && m_PlayerInput.actions["Move"].IsPressed())
            {
                this.m_ObjectRotations[m_Object].MoveObject(m_TurnX, m_TurnY);
            }
            else if (m_CurrentLevel > 1 && m_PlayerInput.actions["VerticalRotation"].IsPressed())
            {
                this.m_ObjectRotations[m_Object].RotateObject(m_TurnX, m_TurnY, true);
            }
            else
            {
                this.m_ObjectRotations[m_Object].RotateObject(m_TurnX, m_TurnY, false);
            }
        }
    }

    public void PauseTutorial(int tuto)
    {
        m_IsPaused = !m_IsPaused;
        if (m_IsPaused)
        {
            m_ScenesManager.ShowTutorial(tuto);
            Time.timeScale = 0f;
        }
        else
        {
            m_ScenesManager.HideGameMenu();
            Time.timeScale = 1f;
        }
    }

    public void PauseGame()
    {
        m_IsPaused = !m_IsPaused;
        if (m_IsPaused)
        {
            m_ScenesManager.ResetMainMenu();
            Time.timeScale = 0f;
        }
        else 
        {
            m_ScenesManager.HideGameMenu();
            Time.timeScale = 1f;
        }
    }

    public void EndLevel()
    {
        float lvl = PlayerPrefs.GetFloat("CurrentLevel", 1f);
        Debug.Log("Level " + lvl + " completed");
        switch (lvl)
        {
            case < 2:
                PlayerPrefs.SetFloat("Stage1", lvl + 0.1f);
                if (lvl == 1.1f)
                {
                    PlayerPrefs.SetFloat("Stage2", 2.1f);
                }
                break;
            case < 3:
                PlayerPrefs.SetFloat("Stage2", lvl + 0.1f);
                if (lvl == 2.1)
                {
                    PlayerPrefs.SetFloat("Stage3", 3.1f);
                }
                break;
            case < 4:
                PlayerPrefs.SetFloat("Stage3", lvl + 0.1f);
                break;
        }
        PlayerPrefs.SetFloat("UpdateSelectionStage", 1f);
        m_ScenesManager.GoToScene("Menu");
    }

    public void SetObject (int obj)
    {
        m_Object = obj;
    }
}
