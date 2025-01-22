using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private InputActionAsset m_InputActionAsset;
    [SerializeField] private Color m_TutorialColor;
    [SerializeField] private Color m_InformationTitleColor;
    [SerializeField] private Color m_InformationsColor;

    [Header("Game Info")]
    [SerializeField] private TMP_Text m_TutorialPanel;
    [SerializeField] private TMP_Text m_Informations;


    public void ActiveMenu(int tuto)
    {
        switch (tuto)
        {
            case 1:
                SetTutorial1();
                break;
            case 2:
                SetTutorial2();
                break;
            case 3:
                SetTutorial3();
                break;
            default:
                return;
        }
        this.gameObject.SetActive(true);
    }

    public void DesactiveMenu()
    {
        this.gameObject.SetActive(false);
    }

    private string GetBind(string actionName)
    {
        return "<color=\"black\">" + m_InputActionAsset.FindAction(actionName).bindings[0].effectivePath.Substring(11).ToUpper() + "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationsColor) + ">";
    }

    public void SetTutorial1()
    {
        m_TutorialPanel.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_TutorialColor) + ">";
        m_TutorialPanel.text += "Welcome to level 1! In <i>In-The-Shadows</i>, your goal is to rotate one or two objects to complete each level.";
        m_Informations.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationTitleColor) +">Informations:\n";
        m_Informations.text += "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationsColor) +"><align=left>";
        m_Informations.text += "1. Press and hold the <b>Left Mouse Button</b>, then drag to rotate the object.\n";
        m_Informations.text += "2. Press '" + this.GetBind("Hint") + "' to have a hint of the shadow you need to recreate. The clue can be audible or visual.\n";
    }

    public void SetTutorial2()
    {
        m_TutorialPanel.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_TutorialColor) + ">";
        m_TutorialPanel.text += "Welcome to level 2! In this level, you need to rotate the object in any direction to complete the level.";
        m_Informations.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationTitleColor) +">Informations:\n";
        m_Informations.text += "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationsColor) +"><align=left>";
        m_Informations.text += "1. Press '" + this.GetBind("VerticalRotation") + "' to rotate the object along the vertical axis.\n";
        m_Informations.text += "2. All controls are listed under 'Controls' in the Settings menu.\n";
    }

    public void SetTutorial3()
    {
        m_TutorialPanel.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_TutorialColor) + ">";
        m_TutorialPanel.text += "Welcome to level 3! In this level, you need to position the objects to recreate the shadow after finding the correct rotation.";
        m_Informations.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationTitleColor) +">Informations:\n";
        m_Informations.text += "<color=#" + ColorUtility.ToHtmlStringRGB(m_InformationsColor) +"><align=left>";
        m_Informations.text += "1. Click on the object you want to rotate or move in the rotation check panel. The panel will indicate if the object is properly rotated.\n";
        m_Informations.text += "2. Press the '" + this.GetBind("Move") + "' key to move the object.\n";
        m_Informations.text += "3. If you lose track of your object, press '" + this.GetBind("ResetPos") + "' to reset it to the starting position.\n";
    }

}
