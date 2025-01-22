
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public static T InitTypes<T>(T value, string from) where T : Object
    {
        if (value == null)
        {
            value = FindAnyObjectByType<T>(FindObjectsInactive.Include);
            if (value == null)
            {
                Debug.LogError("Type '" + value.GetType() + "' not found in '" + from + "'");
                QuitGame();
            }
        }
        return value;
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Map each level to its direct unlockable levels
    public static Dictionary<float, List<float>> levelUnlocks = new Dictionary<float, List<float>>()
    {
        { 1.1f, new List<float> { 1.2f, 2.1f } },
        { 1.2f, new List<float> { 1.3f } },
        { 1.3f, new List<float>() },         // Unlocks nothing
        { 2.1f, new List<float> { 2.2f, 3.1f } },
        { 2.2f, new List<float> { 2.3f } },
        { 2.3f, new List<float>() },         // Unlocks nothing
        { 3.1f, new List<float> { 3.2f } },
        { 3.2f, new List<float> { 3.3f } },
        { 3.3f, new List<float>() }          // Unlocks nothing
    };

    // Map each level to its name
    public static Dictionary<float, string> m_LevelsName = new Dictionary<float, string>()
    {
        { 1.1f, "Fat Thing" },
        { 1.2f, "What a Neck!" },
        { 1.3f, "Gougou Gagak" },
        { 2.1f, "It's tea time" },
        { 2.2f, "Ride the puff" },
        { 2.3f, "Snowball" },
        { 3.1f, "Atlas" },
        { 3.2f, "Level 3-2" },
        { 3.3f, "The answer!" }
    };
}