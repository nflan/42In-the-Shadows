using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelection : MonoBehaviour
{
    [SerializeField] private List<StageInformations> m_StageSelectionPanel = new List<StageInformations>();
    private bool m_TestMode = false;

    void Start()
    {
        if (m_StageSelectionPanel.Count == 0)
        {
            foreach (StageInformations child in GetComponentsInChildren<StageInformations>())
            {
                if (child.gameObject.activeSelf)
                {
                    m_StageSelectionPanel.Add(child);
                }
            }
            if (m_StageSelectionPanel.Count == 0)
            {
                Debug.LogError("Stage Selection Panel is not set in " + this.name);
                return;
            }
        }
    }

    void OnEnable()
    {
        m_TestMode = PlayerPrefs.GetInt("TestMode", -1) == 1;
        if (!m_TestMode)
        {
            this.UpdateStageInfo();
        }
        else
        {
            this.UnlockAllStages();
        }
    }

    private void UnlockAllStages()
    {
        foreach (StageInformations stage in m_StageSelectionPanel)
        {
            stage.UnlockStage();
        }
    }

    public void UpdateStageInfo()
    {
        float time = PlayerPrefs.GetFloat("UpdateSelectionStage", 0f);
        float currLevel = PlayerPrefs.GetFloat("CurrentLevel", 0f);
        List<float> nextLevels = new List<float>();

        if (time > 0 && Tools.levelUnlocks.ContainsKey(currLevel))
        {
            nextLevels = Tools.levelUnlocks[currLevel];
        }
        foreach (StageInformations stage in m_StageSelectionPanel)
        {
            if (nextLevels.Contains(stage.m_StageNumber))
            {
                stage.UnlockStage(time);
            }
            else if (stage.GetMaxUnlockLevel(stage.m_StageNumber) >= stage.m_StageNumber)
            {
                stage.UnlockStage();
            }
        }
        PlayerPrefs.SetFloat("UpdateSelectionStage", 0f);
    }

    public void PauseRotationOfStages()
    {
        foreach (StageInformations stage in m_StageSelectionPanel)
        {
            stage.PauseRotation();
        }
    }
    public void UnpauseRotationOfStages()
    {
        foreach (StageInformations stage in m_StageSelectionPanel)
        {
            stage.UnpauseRotation();
        }
    }
    public void ResetSave()
    {
        foreach (StageInformations stage in m_StageSelectionPanel)
        {
            stage.ResetSave();
        }
    }
}
