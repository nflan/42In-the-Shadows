using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageInformations : IStage
{
    [Header("Stage Info Bis")]
    [SerializeField] public bool m_isLocked = true;
    [SerializeField] private Sprite m_StageImage;

    [Header("Stage Completion")]
    [SerializeField] private Material m_OpenMaterial;
    [SerializeField] private Material m_LockedMaterial;

    [Header("Game Objects")]
    [SerializeField] private GameObject m_StageCube;
    [SerializeField] private StagePanelSelection m_StagePanelSelection;
    [SerializeField] private ScenesManager m_ScenesManager;
    [SerializeField] private GameObject m_LinkedRoad;


    void Start()
    {
        if (m_StageName == null)
        {
            Debug.LogError("Stage Name is not set in " + this.name);
            return;
        }
        if (m_StageRewards == null)
        {
            Debug.LogError("Stage Rewards is not set in " + this.name);
            return;
        }
        if (m_StageCube == null)
        {
            this.m_StageCube = this.transform.Find("StageCube").gameObject;
            if (!this.m_StageCube)
            {
                Debug.LogError("Stage Cube is not set in " + this.name);
                return;
            }
        }
        m_ScenesManager = Tools.InitTypes(m_ScenesManager, this.name);
        m_StagePanelSelection = Tools.InitTypes(m_StagePanelSelection, this.name);
    }

    void OnDisable()
    {
        this.m_isLocked = true;

        if (m_LinkedRoad)
        {
            m_LinkedRoad.GetComponent<RoadController>().ResetColor();
        }
        Renderer stageRenderer = m_StageCube.GetComponent<Renderer>();
        stageRenderer.material.CopyPropertiesFromMaterial(m_LockedMaterial);
        PauseRotation();
    }

    public void SetCurrentLevel()
    {
        PlayerPrefs.SetFloat("CurrentLevel", this.m_StageNumber);
    }

    public int GetStars()
    {
        float time = PlayerPrefs.GetFloat(this.m_StageName + this.m_StageNumber, 0f);
        int stars = 0;
        if (time > 0.000001f)
        {
            for (int i = 0; i < m_StageRewards.Count; i++)
            {
                if (time <= m_StageRewards[i])
                {
                    stars++;
                }
            }
        }
        return stars;
    }

    public void DisplayInformations()
    {
        if (!this.m_isLocked)
        {
            m_StagePanelSelection.SetStageData(m_StageName, m_StageNumber);
            m_StagePanelSelection.SetStageImage(m_StageImage);
            m_StagePanelSelection.SetRewards(m_StageRewards, PlayerPrefs.GetFloat(m_StageName + m_StageNumber, 0f));
            m_StagePanelSelection.gameObject.SetActive(true);
        }
    }

    public void PauseRotation()
    {
        this.GetComponent<Button>().interactable = false;
        this.m_StageCube.GetComponent<Rotator>().ResetOriginal();
        this.m_StageCube.GetComponent<Rotator>().enabled = false;
        this.m_StageCube.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void UnpauseRotation()
    {
        if (!this.m_isLocked)
        {
            this.GetComponent<Button>().interactable = true;
            this.m_StageCube.GetComponent<Rotator>().enabled = true;
        }
    }

    public void ToggleLockImage(float time = 0f)
    {
        StartCoroutine(ActivateRoad(!m_isLocked, time));
    }

    private IEnumerator ActivateRoad(bool active, float time = 0f)
    {
        if (m_LinkedRoad)
        {
            yield return StartCoroutine(m_LinkedRoad.GetComponent<RoadController>().UpdateRoadColor(active, time));
        }

        // Update materials and rotation based on the state
        Renderer stageRenderer = m_StageCube.GetComponent<Renderer>();
        stageRenderer.material.CopyPropertiesFromMaterial(active ? m_OpenMaterial : m_LockedMaterial);

        if (active)
            UnpauseRotation();
        else
            PauseRotation();
    }

    public void UnlockStage(float time = 0f)
    {
        this.m_isLocked = false;
        this.ToggleLockImage(time);
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey(this.m_StageName + this.m_StageNumber);
    }
}
