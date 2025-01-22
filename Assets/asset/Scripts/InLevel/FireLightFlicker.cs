using UnityEngine;

public class FireLightFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 2f;
    public float maxIntensity = 5f;
    public float flickerSpeed = 0.1f;

    void Update()
    {
        fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0f));
    }
}
