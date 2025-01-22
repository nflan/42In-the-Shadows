using UnityEngine;

public class GravityReset : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Store the object's starting position
        initialPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object collides with the floor
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Reset the object's position 12 units above its start point
            transform.position = new Vector3(initialPosition.x, Random.Range(13f, 25f), initialPosition.z);

            // Reset velocity to stop continuous falling force
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
