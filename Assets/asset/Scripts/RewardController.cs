using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    [Header("Rewards")]
    [SerializeField] private List<GameObject> m_Stars;
    [SerializeField] private List<TMP_Text> m_Texts;
    [SerializeField] private Sprite m_StarFilled;
    [SerializeField] private Sprite m_StarEmpty;

    // Start is called before the first frame update
    void Awake()
    {
        if (m_Stars.Count == 0 || m_Texts.Count == 0)
        {
            foreach (Transform child in this.transform)
            {
                foreach (Transform star in child)
                {
                    if (star.name == "Image")
                    {
                        if (!m_Stars.Contains(star.gameObject))
                            m_Stars.Add(star.gameObject);
                    }
                    else if (star.name == "Text")
                    {
                        if (!m_Texts.Contains(star.GetComponent<TMP_Text>()))
                            m_Texts.Add(star.GetComponent<TMP_Text>());
                    }
                }
            }
        }
        if (m_Stars.Count == 0 || m_Texts.Count == 0)
        {
            Debug.LogError("One or more rewards are not set in " + this.name);
            return;
        }
    }

    public void SetReward(int reward, float scoreToDo, float scoreDone = 0f)
    {
        m_Texts[reward].text = scoreToDo.ToString();
        if (scoreDone < 0.0000001f || scoreDone > scoreToDo)
        {
            m_Stars[reward].GetComponent<Image>().sprite = m_StarEmpty;
        }
        else if (scoreDone <= scoreToDo)
        {
            m_Stars[reward].GetComponent<Image>().sprite = m_StarFilled;
        }
    }
}
