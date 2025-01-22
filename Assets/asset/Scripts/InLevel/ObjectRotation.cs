using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private bool    m_Finish = false;
    private Vector3 m_InitialPosition;

    [Header("Object Information")]
    [SerializeField] private List<CheckPosition> m_CheckPositions;

    [Header("Rotation Settings")]
    [SerializeField] private float m_Threshold;
    [SerializeField] private float m_Speed = 60f; // Rotation/Movement speed

    [Header("Components")]
    [SerializeField] private Transform   m_LightSource;
    [SerializeField] public GameObject   m_Canva;
    [SerializeField] private List<Transform> m_TriggersRotation;
    [SerializeField] private GameObject m_RotationCheck;

    void Start()
    {
        m_InitialPosition = transform.position;
    }

    // Smooth Movement
    public void MoveObject(float x, float y)
    {
        Vector3 targetPosition = new Vector3(
            transform.position.x,
            transform.position.y + y * m_Speed * Time.deltaTime * 10,
            transform.position.z + x * m_Speed * Time.deltaTime * 10
        );
        transform.position = targetPosition;
    }

    public void RestartPos()
    {
        transform.position = m_InitialPosition;
    }

    // Smooth Rotation
    public void RotateObject(float x, float y, bool isVertical = true)
    {
        Vector3 right = this.transform.parent.right;
        Vector3 up = this.transform.parent.up;

        float turnY = y * m_Speed;
        float turnX = x * m_Speed;

        if (isVertical)
        {
            transform.localRotation = Quaternion.AngleAxis(turnY, right) * transform.rotation;
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(-turnX, up) * transform.rotation;
        }
    }

    void Update()
    {
        if (IsSameDirection() && IsGoodPosition())
        {
            Debug.Log("name = " + this.name + " = Finish");
            this.m_Finish = true;
        }
        else
        {
            if (IsSameDirection())
                Debug.Log("name = " + this.name + " = Same direction");
            if (IsGoodPosition())
                Debug.Log("name = " + this.name + " = Good position");
            this.m_Finish = false;
        }
    }

    private bool IsGoodPosition()
    {
        foreach (CheckPosition m_CheckPosition in m_CheckPositions)
        {
            if (!m_CheckPosition.GetGoodPosition())
            {
                return false;
            }
        }
        return true;
    }

    public bool GetFinish()
    {
        return m_Finish;
    }

    private bool IsSameDirection()
    {
        float fit = 0;
        float threshold = 0;
        foreach (Transform m_TriggerRotation in m_TriggersRotation)
        {
            fit = Vector3.Dot(m_TriggerRotation.forward, m_LightSource.transform.rotation * Vector3.back);
            threshold = Mathf.Cos(m_Threshold * Mathf.Deg2Rad);
            if (fit >= threshold)
            {
                if (this.m_RotationCheck != null && !this.m_RotationCheck.activeSelf)
                {
                    this.m_RotationCheck.SetActive(true);
                }
                return true;
            }
        }
        if (this.m_RotationCheck != null && this.m_RotationCheck.activeSelf)
        {
            this.m_RotationCheck.SetActive(false);
        }

        return false;
    }
}
