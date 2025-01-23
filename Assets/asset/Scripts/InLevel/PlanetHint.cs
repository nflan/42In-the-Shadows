using System.Collections.Generic;
using UnityEngine;

public class PlanetHint : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private float m_TimeBeforeDisable = 3f;
    [SerializeField] private float m_TimeOnOff = 0.5f;

    [Header("Game Info")]
    [SerializeField] private List<GameObject> m_GlowHints = new List<GameObject>();

    void OnEnable()
    {
        InvokeRepeating("GlowOnOff", 0f, m_TimeOnOff);
        Invoke("GlowOff", m_TimeBeforeDisable);
    }
    void OnDisable()
    {
        CancelInvoke("GlowOnOff");
        GlowOff();
    }

    private void GlowOnOff()
    {
        foreach (GameObject glowHint in m_GlowHints)
        {
            glowHint.SetActive(!glowHint.activeSelf);
        }
    }
    private void GlowOff()
    {
        foreach (GameObject glowHint in m_GlowHints)
        {
            glowHint.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
