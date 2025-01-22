using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private string m_TagToCheck = "Floor";

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(m_TagToCheck))
        {
            Destroy(this.gameObject);
        }
    }
}
