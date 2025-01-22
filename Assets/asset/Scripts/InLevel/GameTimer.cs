using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float m_DeltaTime = 0f;
    [SerializeField] private float timeScale = 60f;
    [SerializeField] private TMP_Text m_ClockText = null;
    
    [Header("Game Info")]
    [SerializeField] private InputController m_InputController = null;
    [SerializeField] public bool m_isLoose = false;

    void Start()
    {
        if (!m_InputController)
        {
            m_InputController = GameObject.FindObjectOfType<InputController>();
            if (!m_InputController)
            {
                Debug.LogError("InputController not found");
            }
        }
        if (!m_ClockText)
        {
            m_ClockText = this.GetComponent<TMP_Text>();
            if (!m_ClockText)
            {
                Debug.LogError("ClockText not found");
            }
        }
    }

    void Update()
    {
        if (!m_InputController || !m_ClockText)
        {
            Debug.LogError("Error in GameTimer");
            return;
        }
        if (m_InputController.m_IsPaused)
        {
            return;
        }
        m_DeltaTime += Time.deltaTime * timeScale;
        int hours = Mathf.FloorToInt(m_DeltaTime / 3600f);
        int minutes = Mathf.FloorToInt((m_DeltaTime - hours * 3600f) / 60f);
        int seconds = Mathf.FloorToInt(m_DeltaTime - hours * 3600f - minutes * 60f);
        int miliseconds = Mathf.FloorToInt((m_DeltaTime - hours * 3600f - minutes * 60f - seconds) * 100);

        if (hours > 0)
        {
            m_ClockText.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hours, minutes, seconds, miliseconds);
            if (hours > 99)
            {
                m_ClockText.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", 99, 59, 59, 99);
                this.m_isLoose = true;
            }
        }
        else
        {
            m_ClockText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        }
    }

    public float GetTime()
    {
        return m_DeltaTime;
    }
}
