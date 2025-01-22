using TMPro;
using UnityEngine;

public class StagePanelEnd : IStagePanel
{
    [SerializeField] private TMP_Text m_StageCurrentScore;


    public void SetStageCurrentScore(string score)
    {
        m_StageCurrentScore.text = score;
    }
}
