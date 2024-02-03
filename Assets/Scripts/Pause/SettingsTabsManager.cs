using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTabsManager : MonoBehaviour
{
    [SerializeField] private Slider[] soundTabSliders;
    [SerializeField] private TMP_Dropdown[] graphicsTabDropdowns;

    Resolution[] resList;

    // Start is called before the first frame update
    void Start()
    {
        resList = Screen.resolutions;
        SetResolutionDropdonwn();
        LoadAudioSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GRAPHICS TAB METHODS

    public void SetResolution(int resIndex) {
        Screen.SetResolution(resList[resIndex].width, resList[resIndex].height, Screen.fullScreen);
    }
    
    private void SetResolutionDropdonwn() {
        graphicsTabDropdowns[1].ClearOptions();
        List<string> optionsList = new List<string>(resList.Length);
        //string[] optionsList = new string[resList.Length];
        for(int i = 0; i < resList.Length; i++) {
            string option = resList[i].height + "x" + resList[i].width;
            optionsList.Add(option);

            if (resList[i].width == Screen.currentResolution.width && resList[i].height == Screen.currentResolution.height) {
                graphicsTabDropdowns[1].value = i;
                graphicsTabDropdowns[1].RefreshShownValue();
            }

        }
        graphicsTabDropdowns[1].AddOptions(optionsList);
    }

    public void QualityChange(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Fullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    //SOUNDS TAB METHODS
    public void ChangeMasterVolume(float input) {
        //AudioManager._audioManagerInstance._mixer.SetFloat("MasterVolume", Mathf.Log10(input) * 20);  sliderRange(0.0001f,1f)
        AudioManager._audioManagerInstance._mixer.SetFloat("MasterVolume", input);
        PlayerPrefs.SetFloat("MasterVolume", input);
    }

    public void ChangeMusicVolume(float input) {
        //AudioManager._audioManagerInstance._mixer.SetFloat("MusicVolume", Mathf.Log(input);   sliderRange(0.0001f,1f)
        AudioManager._audioManagerInstance._mixer.SetFloat("MusicVolume", input);
        PlayerPrefs.SetFloat("MusicVolume", input);
    }

    public void ChangeSfxVolume(float input) {
        //AudioManager._audioManagerInstance._mixer.SetFloat("SfxVolume", Mathf.Log(input); sliderRange(0.0001f,1f)
        AudioManager._audioManagerInstance._mixer.SetFloat("SfxVolume", input);
        PlayerPrefs.SetFloat("SfxVolume", input);
    }

    private void LoadAudioSettings() {
        soundTabSliders[0].value = PlayerPrefs.GetFloat("MasterVolume");
        soundTabSliders[1].value = PlayerPrefs.GetFloat("MusicVolume");
        soundTabSliders[2].value = PlayerPrefs.GetFloat("SfxVolume");

    }

}
