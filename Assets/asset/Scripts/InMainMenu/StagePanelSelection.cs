using UnityEngine;
using UnityEngine.UI;

public class StagePanelSelection : IStagePanel
{
    [SerializeField] private Image m_StageImage;


    public void SetStageImage(Sprite sprite)
    {
        m_StageImage.sprite = sprite;
    }
}
