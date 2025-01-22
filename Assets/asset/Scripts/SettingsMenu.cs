using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] private GameObject m_SettingMenuObj;
    [Header("Audio")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_VolumeSlider;
    [SerializeField] private TMP_InputField m_VolumeInputField;
    [SerializeField] private Toggle m_MusicToggle;

    [Header("Screen")]
    [SerializeField] private TMP_Dropdown m_ResolutionDropdown;
    [SerializeField] private Toggle m_FullScreenToggle;
    [SerializeField] private TMP_Dropdown m_QualityDropdown;
    private Resolution[] m_Resolutions;

    void Awake()
    {
        InitDropDownResolution();
        InitDropDownQuality();       
    }

    void OnEnable()
    {
        InitDropDownResolution();
        InitDropDownQuality();
    }

    public void ActiveMenu(bool active)
    {
        this.m_SettingMenuObj.SetActive(active);
    }

    private void InitDropDownQuality()
    {
        this.m_QualityDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            options.Add(QualitySettings.names[i]);
            if (QualitySettings.GetQualityLevel() == i)
            {
                if (!PlayerPrefs.HasKey("Quality"))
                {
                    PlayerPrefs.SetInt("Quality", i);
                }
            }
        }

        this.m_QualityDropdown.AddOptions(options);
        this.m_QualityDropdown.value = PlayerPrefs.GetInt("Quality");
        this.m_QualityDropdown.RefreshShownValue();
    }

    public void SetAudioSource(AudioSource audioSource)
    {
        this.m_AudioSource = audioSource;
    }

    private void InitDropDownResolution()
    {
        this.m_Resolutions = Screen.resolutions;

        this.m_ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < m_Resolutions.Length; i++)
        {
            options.Add(m_Resolutions[i].width + " x " + m_Resolutions[i].height);
            if (m_Resolutions[i].width == Screen.currentResolution.width &&
                m_Resolutions[i].height == Screen.currentResolution.height)
            {
                if (!PlayerPrefs.HasKey("Resolution"))
                {
                    PlayerPrefs.SetInt("Resolution", i);
                }
            }

        }

        this.m_ResolutionDropdown.AddOptions(options);
        this.m_ResolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        this.m_ResolutionDropdown.RefreshShownValue();
    }

    public void SetVolumeSlider(string volume)
    {
        if (float.TryParse(volume, out float result))
        {
            if (result < 0)
            {
                result = 0;
            }
            else if (result > 100)
            {
                result = 100;
            }
            PlayerPrefs.SetFloat("Volume", result);
            this.m_VolumeSlider.value = result;
        }
    }
    public void SetVolume(float volume)
    {
        this.m_VolumeInputField.text = Math.Round(volume).ToString();
        if (volume >= 5)
        {
            this.m_AudioMixer.SetFloat("volume", -40f + 40f*(volume/100));
        }
        else
        {
            this.m_AudioMixer.SetFloat("volume", -80f + 80f*(volume/100));
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("FullScreen", isFullscreen ? 1 : 0);
        this.m_FullScreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || (m_Resolutions != null && resolutionIndex >= m_Resolutions.Length))
        {
            return;
        }
        if (PlayerPrefs.GetInt("Resolution") != resolutionIndex)
        {
            PlayerPrefs.SetInt("Resolution", resolutionIndex);
            this.m_ResolutionDropdown.value = resolutionIndex;
        }

        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        // if (qualityIndex < 0 || qualityIndex >= QualitySettings.names.Length)
        // {
        //     return;
        // }
        // if (PlayerPrefs.GetInt("Quality") != qualityIndex)
        // {
        //     PlayerPrefs.SetInt("Quality", qualityIndex);
        //     this.m_QualityDropdown.value = qualityIndex;
        // }

        // QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ToggleMusic(bool toggle)
    {
        StartCoroutine(ToggleMusicCoroutine(toggle));
    }

    private IEnumerator ToggleMusicCoroutine(bool toggle)
    {
        // Wait until the AudioSource is ready
        while (this.m_AudioSource == null)
        {
            yield return new WaitForEndOfFrame();
        }

        // Save the preference
        PlayerPrefs.SetInt("Music", toggle ? 1 : 0);

        // Sync the toggle state
        this.m_MusicToggle.isOn = toggle;

        // Play or stop music
        if (toggle)
        {
            this.m_AudioSource.Play();
        }
        else
        {
            this.m_AudioSource.Stop();
        }
    }

}
