using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Button _currentTab;

    [SerializeField] private GameObject _audioSettings;
    [SerializeField] private GameObject _videoSettings;


    // Start is called before the first frame update
    void Start()
    {
        if(_currentTab != null)     _currentTab.Select();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTabPressed() {
        Button _tab = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        
        if (_currentTab == _tab) return;
        _currentTab.interactable = true;
        EventSystem.current.SetSelectedGameObject(null);

        _tab.Select();
        _tab.interactable = false;
        _currentTab = _tab;
    }

    private void InitAudioSettings() {
        Slider[] _sliders = _audioSettings.GetComponentsInChildren<Slider>();
        float x;

        _mixer.GetFloat("MasterVolume", out x);
        _sliders[0].value = x;

        _mixer.GetFloat("MusicVolume", out x);
        _sliders[1].value = x;

        _mixer.GetFloat("EffectsVolume", out x);
        _sliders[2].value = x;
    }

    private void InitVideoSettings() {
        TMP_Dropdown[] _dropdowns = _videoSettings.GetComponentsInChildren<TMP_Dropdown>();
        List<String> tempList = new List<String>();

        foreach(Resolution r in Screen.resolutions) {
            tempList.Add(r.width + "x" + r.height);
        }
        _dropdowns[0].AddOptions(tempList);


    }

    public void OnContinuePress() {
        //Load last active scene
    }

    public void OnQuitPress() {
        Application.Quit();
    }

    public void SetMasterVolume(float val) {
        _mixer.SetFloat("Master", val);
    }
    public void SetMusicVolume(float val) {
        _mixer.SetFloat("Music", val);
    }
    public void SetEffectsVolume(float val) {
        _mixer.SetFloat("Effects", val);
    }

}
