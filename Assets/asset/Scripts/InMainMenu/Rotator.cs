using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float m_RotationSpeed = 20.0f;
    [SerializeField] private Vector3 m_RotationAxis = Vector3.up;
    private Vector3 m_OriginalScale = new Vector3(120.0f, 120.0f, 120.0f);
    private float m_OriginalSpeed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_RotationAxis, m_RotationSpeed * Time.deltaTime);
    }

    void OnMouseOver()
    {
        if (isActiveAndEnabled)
        {
            m_RotationSpeed = m_OriginalSpeed * 2.0f;
            this.transform.localScale = m_OriginalScale * 1.2f;
        }
    }
    void OnMouseExit()
    {
        if (isActiveAndEnabled)
        {
            m_RotationSpeed = m_OriginalSpeed;
            this.transform.localScale = m_OriginalScale;
        }
    }

    public void ResetOriginal()
    {
        m_RotationSpeed = m_OriginalSpeed;
        this.transform.localScale = m_OriginalScale;        
    }
}
