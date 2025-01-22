using UnityEngine;

public class HintManager : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] private AudioSource m_AudioHint;
    [SerializeField] private GameObject m_PhysicalHint;

    public void Hint()
    {
        if (m_AudioHint)
        {
            m_AudioHint.Play();
        }
        if (m_PhysicalHint)
        {
            m_PhysicalHint.SetActive(true);
        }
    }
}
