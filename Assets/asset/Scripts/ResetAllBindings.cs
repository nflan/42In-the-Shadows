using UnityEngine;
using UnityEngine.InputSystem;


public class ResetAllBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputActions;

    public void ResetBindings()
    {
        foreach (InputActionMap map in m_InputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("rebinds");
    }
}
