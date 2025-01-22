using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    [Header("Object Informations")]
    [SerializeField] private Vector3 m_StartPosition = new Vector3(-10, -5, 0);
    [SerializeField] private Vector2 m_RangeVelocityX;
    [SerializeField] private Vector2 m_RangeVelocityY;
    [SerializeField] private Vector2 m_RangeVelocityZ;
    [SerializeField] private float m_DistanceMax = 300f;

    void OnEnable()
    {
        transform.position = m_StartPosition;
        this.GetComponent<Rigidbody>().velocity = new Vector3(
            Random.Range(m_RangeVelocityX.x, m_RangeVelocityX.y),
            Random.Range(m_RangeVelocityY.x, m_RangeVelocityY.y),
            Random.Range(m_RangeVelocityZ.x, m_RangeVelocityZ.y)
        );
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, m_StartPosition) > m_DistanceMax)
        {
            this.gameObject.SetActive(false);
        }
    }
}
