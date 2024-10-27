using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class TestPause : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;

    private UIDocument doc;

    private List<Button> _buttons = new List<Button>();
    private List<Button> _optionsTabs = new List<Button>();
    private Button _back;

    private List<VisualElement> _pages = new List<VisualElement>();
    private List<VisualElement> _optionsContainer = new List<VisualElement>();

    private VisualElement _currentPage;
    private VisualElement _currentOptions;

    // Start is called before the first frame update
    void Start()
    {
        doc = GetComponent<UIDocument>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPause() {
        _buttons = doc.rootVisualElement.Query("PauseButtons").Children<Button>().ToList();
        _optionsTabs = doc.rootVisualElement.Query("OptionsTabs").Children<Button>().ToList();
        _back = doc.rootVisualElement.Q<Button>("BackButton");

        _optionsContainer = doc.rootVisualElement.Query("OptionsContainer").Children<VisualElement>().ToList();

        InitVideoSettings();
        InitAudioSettings();
    }

    private void InitVideoSettings() {
        if (_optionsContainer[0] == null) return;

        List<VisualElement> temp = _optionsContainer[0].Children().ToList();
        
        DropdownField _resDropdown = temp[0] as DropdownField;
        DropdownField _qualityDropdown = temp[1] as DropdownField;
        RadioButton _fullscreen = temp[2] as RadioButton;

        foreach(Resolution r in Screen.resolutions) {
            _resDropdown.choices.Add(r.width + "x" + r.height);
        }
        _resDropdown.value = _resDropdown.choices.Last();

        _qualityDropdown.choices = QualitySettings.names.ToList();
        _qualityDropdown.value = _qualityDropdown.choices.Last();

        _fullscreen.value = Screen.fullScreen;

        _resDropdown.RegisterCallback<ChangeEvent<string>>(SetResolution);
        _qualityDropdown.RegisterCallback<ChangeEvent<int>>(SetQuality);
        _fullscreen.RegisterCallback<ChangeEvent<bool>>(SetFullscreen);
    }

    //video methods
    #region
    public void SetResolution(ChangeEvent<string> evnt) {
        foreach(Resolution r in Screen.resolutions) {
            if (r.ToString() == evnt.newValue) Screen.SetResolution(r.width, r.height, Screen.fullScreen); 
        }
    }
    public void SetQuality(ChangeEvent<int> evnt) {
        QualitySettings.SetQualityLevel(evnt.newValue);
    }
    public void SetFullscreen(ChangeEvent<bool> evnt) {
        Screen.fullScreen = evnt.newValue;
    }
    #endregion 

    private void InitAudioSettings() {
        if (_optionsContainer[1] == null) return;

        List<VisualElement> temp = _optionsContainer[1].Children().ToList();
        Slider _masterVolume = temp[0] as Slider;
        Slider _musicVolume = temp[1] as Slider;
        Slider _effectsVolume = temp[2] as Slider;

        float x;

        _mixer.GetFloat("Master", out x);
        _masterVolume.value = x;

        _mixer.GetFloat("Music", out x);
        _musicVolume.value = x;

        _mixer.GetFloat("Effects", out x);
        _effectsVolume.value = x;

        _masterVolume.RegisterCallback<ChangeEvent<float>>(SetMasterVolume);
        _musicVolume.RegisterCallback<ChangeEvent<float>>(SetMusicVolume);
        _effectsVolume.RegisterCallback<ChangeEvent<float>>(SetEffectsVolume);
    }

    //audio methods
    #region
    public void SetMasterVolume(ChangeEvent<float> evnt) {
        _mixer.SetFloat("Master", evnt.newValue);
    }
    public void SetMusicVolume(ChangeEvent<float> evnt) {
        _mixer.SetFloat("Music", evnt.newValue);
    }
    public void SetEffectsVolume(ChangeEvent<float> evnt) {
        _mixer.SetFloat("Effects", evnt.newValue);
    }
    #endregion 
}
