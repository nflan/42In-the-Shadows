using UnityEngine;

public class PhysicalHint : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private float m_TimeBeforeDisable = 3f;
    private float m_DeltaTime = 0f;

    void OnEnable()
    {
        m_DeltaTime = 0f;
    }

    void OnDisable()
    {
        m_DeltaTime = 0f;
    }

    void Update()
    {
        m_DeltaTime += Time.deltaTime;
        if (m_DeltaTime >= m_TimeBeforeDisable)
        {
            this.gameObject.SetActive(false);
        }
    }
}
