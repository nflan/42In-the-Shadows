using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelStars", menuName = "LevelStars")]
public class LevelStars : ScriptableObject
{
    [SerializeField] public List<float> m_Level11;
    [SerializeField] public List<float> m_Level12;
    [SerializeField] public List<float> m_Level13;
    [SerializeField] public List<float> m_Level21;
    [SerializeField] public List<float> m_Level22;
    [SerializeField] public List<float> m_Level23;
    [SerializeField] public List<float> m_Level31;
    [SerializeField] public List<float> m_Level32;
    [SerializeField] public List<float> m_Level33;

    private void OnEnable()
    {
        SetLevelStars(m_Level11, 5.5f, 10.5f, 15.75f);
        SetLevelStars(m_Level12, 5.5f, 10.5f, 15.75f);
        SetLevelStars(m_Level13, 5.5f, 10.5f, 15.75f);
        SetLevelStars(m_Level21, 11f, 21f, 31.5f);
        SetLevelStars(m_Level22, 11f, 21f, 31.5f);
        SetLevelStars(m_Level23, 11f, 21f, 31.5f);
        SetLevelStars(m_Level31, 20f, 25f, 30f);
        SetLevelStars(m_Level32, 20f, 25f, 30f);
        SetLevelStars(m_Level33, 20f, 25f, 30f);
    }

    private void SetLevelStars(List<float> levelStars, float a, float b, float c)
    {
        if (levelStars.Count > 0)
        {
            return ;
        }
        levelStars.Add(a);
        levelStars.Add(b);
        levelStars.Add(c);
    }
    
    public List<float> GetLevelStars(float stageNb)
    {
        switch (stageNb)
        {
            case 1.1f:
                return m_Level11;
            case 1.2f:
                return m_Level12;
            case 1.3f:
                return m_Level13;
            case 2.1f:
                return m_Level21;
            case 2.2f:
                return m_Level22;
            case 2.3f:
                return m_Level23;
            case 3.1f:
                return m_Level31;
            case 3.2f:
                return m_Level32;
            case 3.3f:
                return m_Level33;
            default:
                return null;
        }
    }
}
