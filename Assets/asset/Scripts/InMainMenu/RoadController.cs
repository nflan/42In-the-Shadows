using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadController : MonoBehaviour
{
    [Header("Road Settings")]
    [SerializeField] private List<GameObject> m_Rods = new List<GameObject>();
    [SerializeField] private Color m_LockColor = Color.grey;
    [SerializeField] private Color m_UnlockColor = Color.white;

    void Start()
    {
        if (m_Rods.Count == 0)
        {
            foreach (Transform child in transform)
            {
                m_Rods.Add(child.gameObject);
            }
        }
    }

    public IEnumerator UpdateRoadColor(bool active, float time = 0f)
    {
        Color targetColor = active ? m_UnlockColor : m_LockColor;

        foreach (GameObject road in m_Rods)
        {
            yield return StartCoroutine(ChangeColour(road, targetColor, time));

            // Optional delay between each road coloring for a smoother effect
            yield return new WaitForSeconds(time / 10);
        }
    }

    private IEnumerator ChangeColour(GameObject target, Color targetColor, float time)
    {
        Image image = target.GetComponent<Image>(); // Cache the Image component
        Color startColor = image.color;
        float elapsed = 0f;

        while (elapsed < time)
        {
            image.color = Color.Lerp(startColor, targetColor, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        image.color = targetColor; // Ensure final color is set
    }

    public void ResetColor()
    {
        foreach (GameObject road in m_Rods)
        {
            road.GetComponent<Image>().color = m_LockColor;
        }
    }
}
