using System.Collections.Generic;
using UnityEngine;

public class IStage : MonoBehaviour
{
    [Header("Stage Info")]
    [SerializeField] public string m_StageName = null;
    [SerializeField] public float m_StageNumber = 0f;
    [SerializeField] public List<float> m_StageRewards = null;
    [SerializeField] private LevelStars m_LevelStars = null;

    void Awake()
    {
        if (Tools.m_LevelsName.ContainsKey(m_StageNumber))
        {
            m_StageName = Tools.m_LevelsName[m_StageNumber];
        }
        else
        {
            m_StageName = "Stage " + m_StageNumber;
        }
        m_StageRewards = m_LevelStars.GetLevelStars(m_StageNumber);
    }

    public float GetMaxUnlockLevel(float stageNumber)
    {
        if (stageNumber < 2f)
        {
            return PlayerPrefs.GetFloat("Stage1", 0f);
        }
        else if (stageNumber < 3f)
        {
            return PlayerPrefs.GetFloat("Stage2", 0f);
        }
        else
        {
            return PlayerPrefs.GetFloat("Stage3", 0f);
        }
    }

    protected void SetUnlockedLevel(float stageNumber)
    {
        if (stageNumber < 2f)
        {
            PlayerPrefs.SetFloat("Stage1", stageNumber);
        }
        else if (stageNumber < 3f)
        {
            PlayerPrefs.SetFloat("Stage2", stageNumber);
        }
        else
        {
            PlayerPrefs.SetFloat("Stage3", stageNumber);
        }
        PlayerPrefs.Save();
    }
}