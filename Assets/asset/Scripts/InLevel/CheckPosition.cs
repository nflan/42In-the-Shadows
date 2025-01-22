using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    private bool m_GoodPosition = false;
    [Header("Game Info")]
    [SerializeField] private string m_Tag = "TrackingObject";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == m_Tag)
        {
            m_GoodPosition = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == m_Tag)
        {
            m_GoodPosition = false;
        }
    }

    public bool GetGoodPosition()
    {
        return m_GoodPosition;
    }
}
