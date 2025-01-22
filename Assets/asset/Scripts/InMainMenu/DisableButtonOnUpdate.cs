using UnityEngine;
using UnityEngine.UI;

public class DisableButtonOnUpdate : MonoBehaviour
{
    [SerializeField] private Button m_Button;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey("ActiveSave"))
        {
            m_Button.interactable = false;
        }
        else
        {
            m_Button.interactable = true;
        }
    }
}
