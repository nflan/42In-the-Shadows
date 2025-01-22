using UnityEngine;

public class LevelInformations : IStage
{
    [SerializeField] private StagePanelEnd m_StagePanelEnd;
    [SerializeField] private HintManager m_HintManager;
    [SerializeField] private GameTimer m_GameTimer;
    private bool m_TestMode = false;

    void Start()
    {
        m_StagePanelEnd = Tools.InitTypes(m_StagePanelEnd, this.name);
        m_GameTimer = Tools.InitTypes(m_GameTimer, this.name);
        m_TestMode = PlayerPrefs.GetInt("TestMode") == 1;
    }

    public void EndStage()
    {
        float time = m_GameTimer.GetTime();
        m_StagePanelEnd.SetStageData(m_StageName, m_StageNumber);
        m_StagePanelEnd.SetStageCurrentScore(time.ToString());
        // Set personal best and rewards
        m_StagePanelEnd.SetRewards(m_StageRewards, time);
        m_StagePanelEnd.gameObject.SetActive(true);
    
        if (time < 0.1 || !CheckUnlockStage(time) || m_TestMode)
        {
            return ;
        }
        if (PlayerPrefs.GetFloat(this.m_StageName, 0f) < 0.000001f)
        {
            PlayerPrefs.SetFloat("UpdateSelectionStage", 1f);
            this.UnlockNextStage();
        }
    }

    public void Hint()
    {
        this.m_HintManager.Hint();
    }

    private bool CheckUnlockStage(float time)
    {
        foreach (float reward in m_StageRewards)
        {
            if (reward >= time)
            {
                if (this.GetMaxUnlockLevel(this.m_StageNumber) == this.m_StageNumber)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void UnlockNextStage()
    {
        if (Tools.levelUnlocks.ContainsKey(this.m_StageNumber))
        {
            foreach (float nextLevel in Tools.levelUnlocks[this.m_StageNumber])
            {
                float unlockedLevel = this.GetMaxUnlockLevel(nextLevel);

                if (unlockedLevel < nextLevel)
                {
                    this.SetUnlockedLevel(nextLevel);
                }
            }
        }
    }
}