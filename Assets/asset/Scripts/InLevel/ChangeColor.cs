using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [Header("Component Infos")]
    [SerializeField] private Image m_Image;
    [SerializeField] private Color m_BaseColor;
    [SerializeField] private Color m_NewColor;
    [SerializeField] private bool m_NewColorOnStart = false;

    void Start()
    {
        if (!m_Image)
        {
            m_Image = GetComponent<Image>();
        }
        m_BaseColor = m_Image.color;
        if (m_NewColorOnStart)
        {
            SetNewImageColor();
        }
    }

    public void SetNewImageColor()
    {
        m_Image.color = m_NewColor;
    }
    public void ResetImageColor()
    {
        m_Image.color = m_BaseColor;
    }
}
