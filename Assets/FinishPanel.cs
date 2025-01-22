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
        if (PlayerPrefs.GetFloat("Stage3", 0f) > 3.31f)
        {
            m_FinishPanel.SetActive(true);
            m_FinishText.text = "Congratulations!\n";
            m_FinishText.text += "You got ";
            m_FinishText.text += "<color=\"black\">" + this.GetAllStars() + "<color=#323232>";
            m_FinishText.text += " stars. You can now grade me <color=\"black\">125<color=#323232>!";
        }
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
