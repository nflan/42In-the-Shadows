using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IStagePanel : MonoBehaviour
{
    [Header("Stage Info")]
    [SerializeField] private TMP_Text m_StageName;
    [SerializeField] private TMP_Text m_PersonalBest;
    [SerializeField] private RewardController m_RewardController;

    private string m_StageData;


    public void SetStageData(string name, float number)
    {
        m_StageName.text = name;
        m_StageData = name + number;
    }
    public void SetPersonalBest(float time)
    {
        float personalBest = PlayerPrefs.GetFloat(this.m_StageData, -1f);
        if (personalBest < 0.0000001f || personalBest > time)
        {
            if (PlayerPrefs.GetInt("TestMode") == 0)
            {
                PlayerPrefs.SetFloat(this.m_StageData, time);
            }
        }
        else
        {
            time = PlayerPrefs.GetFloat(this.m_StageData);
        }
        m_PersonalBest.text = time.ToString();
    }
    public void SetRewards(List<float> rewards, float scoreDone = 0f)
    {
        this.SetPersonalBest(scoreDone);
        for (int i = 0; i < rewards.Count; i++)
        {
            m_RewardController.SetReward(i, rewards[i], scoreDone);
        }
    }

    public void HideStagePanel()
    {
        this.gameObject.SetActive(false);
    }
}