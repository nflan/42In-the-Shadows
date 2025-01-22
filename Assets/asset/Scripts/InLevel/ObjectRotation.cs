using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private bool    m_Finish = false;
    private Vector3 m_InitialPosition;
    private Vector3 m_InitialRotation;

    [Header("Object Information")]
    [SerializeField] private List<CheckPosition> m_CheckPositions;

    [Header("Rotation Settings")]
    [SerializeField] private float m_Threshold;
    [SerializeField] private float m_MovementSpeed = 12f; // Rotation/Movement speed
    [SerializeField] private float m_RotationSpeed = 180; // Rotation speed

    [Header("Components")]
    [SerializeField] private Transform   m_LightSource;
    [SerializeField] public GameObject   m_Canva;
    [SerializeField] private List<Transform> m_TriggersRotation;
    [SerializeField] private GameObject m_RotationCheck;

    void Start()
    {
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.eulerAngles;
    }

    public void RestartPos()
    {
        transform.position = m_InitialPosition;
        transform.eulerAngles = m_InitialRotation;
    }

    // Smooth Movement
    public void MoveObject(float x, float y)
    {
        Vector3 targetPosition = new Vector3(
            transform.position.x,
            transform.position.y + y * m_MovementSpeed * Time.deltaTime,
            transform.position.z + x * m_MovementSpeed * Time.deltaTime
        );
        transform.position = targetPosition;
    }


    // Smooth Rotation
    public void RotateObject(float x, float y, bool isVertical = true)
    {
        Vector3 right = this.transform.parent.right;
        Vector3 up = this.transform.parent.up;

        float turnY = y * m_RotationSpeed * Time.deltaTime;
        float turnX = x * m_RotationSpeed * Time.deltaTime;

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
            this.m_Finish = true;
        }
        else
        {
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
