using UnityEngine;

public class DirectToCam : MonoBehaviour
{
    public Transform m_Compare;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.forward = m_Compare.transform.rotation * Vector3.back;
        }
    }
}
