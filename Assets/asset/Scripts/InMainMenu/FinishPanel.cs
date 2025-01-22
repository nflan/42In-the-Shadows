using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_FinishPanel;
    [SerializeField] private TMP_Text m_FinishText;
    [SerializeField] private List<StageInformations> m_StageInformations;

    void Start()
    {
        if (CheckFinish())
        {
            m_FinishPanel.SetActive(true);
            m_FinishText.text = "Congratulations!\n";
            m_FinishText.text += "You got ";
            m_FinishText.text += "<color=\"black\">" + this.GetAllStars() + "<color=#323232>";
            m_FinishText.text += " stars. You can now grade me <color=\"black\">125<color=#323232>!";
        }
    }

    private bool CheckFinish()
    {
        foreach (StageInformations stage in m_StageInformations)
        {
            if (stage.GetStars() < 1)
            {
                return false;
            }
        }
        return true;
    }

    private int GetAllStars()
    {
        int stars = 0;
        foreach (StageInformations stage in m_StageInformations)
        {
            stars += stage.GetStars();
        }
        return stars;
    }
    
    public void HideFinishPanel()
    {
        m_FinishPanel.SetActive(false);
    }
}
