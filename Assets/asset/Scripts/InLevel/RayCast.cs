using UnityEngine;

public class RayCast : MonoBehaviour//ScriptableObject
{
    [SerializeField] public float m_Range = 5f;
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private GameObject m_TargetRayCast;

    [SerializeField] private Color forwardColor = Color.yellow;
    [SerializeField] private Color upColor = Color.red;
    [SerializeField] private Color rightColor = Color.green;

    void Update()
    {
        Raycast();
    }

    public void Raycast()
    {
        Ray forward = new Ray(m_TargetRayCast.transform.position, m_TargetRayCast.transform.forward);
        Debug.DrawRay(forward.origin, forward.direction * m_Range, forwardColor);

        Ray up = new Ray(m_TargetRayCast.transform.position, m_TargetRayCast.transform.up);
        Debug.DrawRay(up.origin, up.direction * m_Range, upColor);

        Ray right = new Ray(m_TargetRayCast.transform.position, m_TargetRayCast.transform.right);
        Debug.DrawRay(right.origin, right.direction * m_Range, rightColor);

    }
}