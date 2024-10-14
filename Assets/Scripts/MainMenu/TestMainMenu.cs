using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.Audio;
using System.Collections;
using System.Threading.Tasks;

public class TestMainMenu : MonoBehaviour
{
    private UIDocument doc;

    private List<Button> _mainPageButtons = new List<Button>();
    private List<Button> _optionsTabs = new List<Button>();
    private List<Button> _saveSlots = new List<Button>();
    private Button _back;


    private List<VisualElement> _pages = new List<VisualElement>();
    private List<VisualElement> _optionsContainer = new List<VisualElement>();

    private VisualElement _currentPage;
    private VisualElement _currentOptionsPage;

    [SerializeField] private AudioMixer _mixer;

    private void Awake() {
        doc = GetComponent<UIDocument>();
    }

    // Start is called before the first frame update
    void Start() {
        _pages = doc.rootVisualElement.Query("Pages").Children<VisualElement>().ToList();
        if(_pages != null) _currentPage = _pages[0];

        _optionsContainer = doc.rootVisualElement.Query("OptionsContainer").Children<VisualElement>().ToList();

        _back = doc.rootVisualElement.Q<Button>("BackButton");
        _back.RegisterCallback<ClickEvent>(OnBackSelect);

        InitButtons();
        InitVideoOptions();
        InitAudioOptions();

        MainPageDispatcher();
        OptionsTabsDispatcher();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private async void SwitchPage(VisualElement nextPage) {
        if (nextPage == _currentPage) return;


        _currentPage.AddToClassList("Switch");
        await Task.Delay(300); //0.3s
        _currentPage.RemoveFromClassList("Switch");
        _currentPage.style.display = DisplayStyle.None;

        _back.ToggleInClassList("Switch");
        //_back.style.display = (nextPage == _pages[0]) ? DisplayStyle.None : DisplayStyle.Flex;

        nextPage.style.display = DisplayStyle.Flex;
        _currentPage = nextPage;
    }

    private void InitButtons() {
        _mainPageButtons = doc.rootVisualElement.Query("MainPage").Children<Button>().ToList();
        _optionsTabs = doc.rootVisualElement.Query("OptionsTabs").Children<Button>().ToList();
        _saveSlots = doc.rootVisualElement.Query("OptionsTabs").Children<Button>().ToList();
    }


    private void MainPageDispatcher() {
        if (_mainPageButtons == null) return;
        
        foreach(Button b in _mainPageButtons) {
            switch (b.name) {
                case "NewGame":
                    b.RegisterCallback<ClickEvent>(OnNewGameSelect);
                    break;
                case "LoadGame":
                    b.RegisterCallback<ClickEvent>(OnNewGameSelect); //provvisorio
                    break;
                case "Options":
                    b.RegisterCallback<ClickEvent>(OnOptionsSelect);
                    break;
                case "Quit":
                    b.RegisterCallback<ClickEvent>(OnQuitSelect);
                    break;
            }
        }
    }

    private void OptionsTabsDispatcher() {
        if (_optionsTabs == null) return;

        foreach(Button b in _optionsTabs) {
            b.RegisterCallback<ClickEvent>(TabSwitch);
        }
    }

    //MainPage Methods
    public void OnNewGameSelect(ClickEvent evnt) {
        SwitchPage(_pages[1]);
    }

    public void OnOptionsSelect(ClickEvent evnt) {
        SwitchPage(_pages[2]);
        if (_optionsContainer != null) _currentOptionsPage = _optionsContainer[0];
        if(_optionsTabs != null) _optionsTabs[0].Focus();
    }

    public void OnQuitSelect(ClickEvent evnt) {
        Application.Quit();
    }

    public void OnBackSelect(ClickEvent evnt) {
        SwitchPage(_pages[0]);
    }

    //Options Methods
    public async void TabSwitch(ClickEvent evnt) {
        if (_optionsContainer == null) return;

        for(int i = 0; i < _optionsTabs.Count; i++) {
            if(evnt.currentTarget == _optionsTabs[i]) {
                _optionsTabs[i].Focus();

                if (_currentOptionsPage == _optionsContainer[i]) return;

                _currentOptionsPage.AddToClassList("Switch");
                await Task.Delay(350);
                _currentOptionsPage.RemoveFromClassList("Switch");
                _currentOptionsPage.style.display = DisplayStyle.None;


                _optionsContainer[i].style.display = DisplayStyle.Flex;
                _currentOptionsPage = _optionsContainer[i];
            }
        }
    }

    public void InitVideoOptions() {
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
    public void SetResolution(ChangeEvent<string> input) {
        foreach(Resolution r in Screen.resolutions) {
            if (r.ToString().Equals(input.newValue)) Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        }
    }
    public void SetQuality(ChangeEvent<int> input) {
        QualitySettings.SetQualityLevel(input.newValue);
    }

    public void SetFullscreen(ChangeEvent<bool> input) {
        Screen.fullScreen = input.newValue;
    }

    public void InitAudioOptions() {
        List<VisualElement> temp = _optionsContainer[1].Children().ToList();

        Slider _masterVolume = temp[0] as Slider;
        Slider _musicVolume = temp[1] as Slider;
        Slider _effectsVolume = temp[2] as Slider;
        float x;

        _mixer.GetFloat("MasterVolume", out x);
        _masterVolume.value = x;

        _mixer.GetFloat("MusicVolume", out x);
        _musicVolume.value = x;

        _mixer.GetFloat("EffectsVolume", out x);
        _effectsVolume.value = x;

        _masterVolume.RegisterCallback<ChangeEvent<float>>(SetMasterVolume);
        _musicVolume.RegisterCallback<ChangeEvent<float>>(SetMusicVolume);
        _effectsVolume.RegisterCallback<ChangeEvent<float>>(SetEffectsVolume);
    }
    public void SetMasterVolume(ChangeEvent<float> input) {
        _mixer.SetFloat("MasterVolume", input.newValue);
    }
    public void SetMusicVolume(ChangeEvent<float> input) {
        _mixer.SetFloat("MusicVolume", input.newValue);
    }
    public void SetEffectsVolume(ChangeEvent<float> input) {
        _mixer.SetFloat("EffectsVolume", input.newValue);
    }
}
