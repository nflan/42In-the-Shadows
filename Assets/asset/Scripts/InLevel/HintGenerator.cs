using UnityEngine;

public class HintGenerator : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private GameObject m_PrefabToSpawn = null;
    [SerializeField] private Vector2 m_SpawnRange = new Vector2(0, 0);
    [SerializeField] private Vector2 m_SpawnPositionX = new Vector2(0, 0);
    [SerializeField] private Vector2 m_SpawnPositionY = new Vector2(0, 0);
    [SerializeField] private Vector2 m_SpawnPositionZ = new Vector2(0, 0);

    void OnEnable()
    {
        int random = Random.Range(Mathf.RoundToInt(m_SpawnRange.x), Mathf.RoundToInt(this.m_SpawnRange.y));
        for (int i = 0; i < random; i++)
        {
            GameObject newObject = Instantiate(m_PrefabToSpawn, this.transform.parent.position, this.transform.rotation, this.transform.parent);
            newObject.transform.localPosition = new Vector3(
                Random.Range(this.m_SpawnPositionX.x, this.m_SpawnPositionX.y),
                Random.Range(this.m_SpawnPositionY.x, this.m_SpawnPositionY.y),
                Random.Range(this.m_SpawnPositionZ.x, this.m_SpawnPositionZ.y)
            );
        }
        
        this.gameObject.SetActive(false);
    }
}
