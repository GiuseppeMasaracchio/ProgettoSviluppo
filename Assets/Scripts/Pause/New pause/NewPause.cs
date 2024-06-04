using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class NewPause : MonoBehaviour
{
    private VisualElement root;

    private List<Button> _mainPageButtons;
    private List<Button> _settingsTabs;
    private Button _backButton;

    private List<VisualElement> _pages;
    private List<VisualElement> _settingsContent;

    [SerializeField] private AudioMixer _mixer;

    private VisualElement currentContent = null;

    //Typewriter
    private string line = "";
    [SerializeField] private float charDelay = 0.02f;

    private void OnEnable() {
    }

    private void OnDisable() {
        //ButtonDispose(_settingsTabs);
        //ButtonDispose(_mainPageButtons);
        //_backButton.UnregisterCallback<ClickEvent>(OnBackHandler);
    }

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;

        InitPauseMenu();
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Typewriter<T>(T elem) {
        if (elem is Button) {
            Button temp = elem as Button;
            line = temp.text;
            temp.text = "";
            temp.style.opacity = 100f;
            foreach (char c in line) {
                temp.text += c;
                yield return new WaitForSeconds(charDelay);
            }
            yield return new WaitUntil(() => temp.text.Length == line.Length);
            line = "";
        }
    }

    private T[] FillArray<T>(T[] arr, string name) {
        if(arr is Button[]) {
            List<Button> temp = root.Query(name).Children<Button>().ToList();
            arr = temp.ToArray() as T[];
        }
        if(arr is VisualElement[]) {
            List<VisualElement> temp = root.Query(name).Children<VisualElement>().ToList();
            arr = temp.ToArray() as T[];
        }
        return arr;
    }
    private void ButtonHandlers(List<Button> bList) {
        foreach (Button b in bList) {
            switch (b.name) {
                //MainPage
                case "Resume":
                    b.RegisterCallback<ClickEvent>(OnResumeHandler);
                    break;
                case "Settings":
                    b.RegisterCallback<ClickEvent>(OnSettingsHandler);
                    break;
                case "MainMenu":
                    break;

                //Tabs
                case "Video":
                    b.RegisterCallback<ClickEvent>(OnVideoTab);
                    break;
                case "Audio":
                    b.RegisterCallback<ClickEvent>(OnAudioTab);
                    break;
                case "Controls":
                    break;
            }
        }
    }

    private void ButtonDispose(List<Button> arr) {
        foreach(Button b in arr) {
            switch (b.name) {
                    //MainPage
                case "Resume":
                    b.UnregisterCallback<ClickEvent>(OnResumeHandler);
                    break;
                case "Settings":
                    b.UnregisterCallback<ClickEvent>(OnSettingsHandler);
                    break;
                case "MainMenu":
                    break;
                //SettingsPage
                case "Video":
                    b.UnregisterCallback<ClickEvent>(OnVideoTab);
                    break;
                case "Audio":
                    b.UnregisterCallback<ClickEvent>(OnAudioTab);
                    break;
                case "Controls":
                    break;
            }
        }
    }

    //Handlers
    private void OnResumeHandler(ClickEvent evnt) {
        gameObject.SetActive(false);
    }

    private void OnSettingsHandler(ClickEvent evnt) {
        SwitchPage(_pages[0], _pages[1]);
    }

    private void OnBackHandler(ClickEvent evnt) {
        SwitchPage(_pages[1], _pages[0]);
    }

    private void OnVideoTab(ClickEvent evnt) {
        SwitchPage(currentContent, _settingsContent.First());
        if(!_settingsContent.First().Equals(currentContent))  currentContent = _settingsContent.First();
    }

    private void OnAudioTab(ClickEvent evnt) {
        SwitchPage(currentContent, _settingsContent.ElementAt(1));
        if(!_settingsContent.ElementAt(1).Equals(currentContent))   currentContent = _settingsContent.ElementAt(1);
    }

    private void OnControlsTab(ClickEvent evnt) {
        SwitchPage(currentContent, _settingsContent.ElementAt(2));
        if(!_settingsContent.ElementAt(2).Equals(currentContent))  currentContent = _settingsContent.ElementAt(2);
    }

    private void InitPauseMenu() {
        _mainPageButtons = root.Query("MainPage").Children<Button>().ToList();
        ButtonHandlers(_mainPageButtons);

        _settingsTabs = root.Query("TabsContainer").Children<Button>().ToList();
        ButtonHandlers(_settingsTabs);

        _pages = root.Query("Body").Children<VisualElement>().ToList();

        _settingsContent = root.Query("ContentContainer").Children<VisualElement>().ToList();
        InitVideoSettings(_settingsContent[0].Children().ToList());
        InitAudioSettings(_settingsContent[1].Children().ToList());

        _backButton = root.Q("Back") as Button;
        _backButton.RegisterCallback<ClickEvent>(OnBackHandler);
    }

    private void InitVideoSettings(List<VisualElement> setList) {
        InitResolutionDropdown(setList[0] as DropdownField);
        setList[0].RegisterCallback<ChangeEvent<string>>(ResolutionHandler);

        InitQualityDropdown(setList[1] as DropdownField);
        setList[1].RegisterCallback<ChangeEvent<string>>(QualityHandler);

        setList[2].RegisterCallback<ChangeEvent<bool>>(FullscreenToggle);
    }

    private void InitAudioSettings(List<VisualElement> setList) {
        setList[0].RegisterCallback<ChangeEvent<float>>(MusicSlider);
        setList[1].RegisterCallback<ChangeEvent<float>>(EffectsSlider);
    }

    private void MusicSlider(ChangeEvent<float> evnt) {
        _mixer.SetFloat("MusicVolume", evnt.newValue);
    }

    private void EffectsSlider(ChangeEvent<float> evnt) {
        _mixer.SetFloat("SfxVolume", evnt.newValue);
    }

    private void InitResolutionDropdown(DropdownField input) {
        input.value = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
        foreach(Resolution r in Screen.resolutions) {
            input.choices.Add(r.width + "x" + r.height);
        }
    }

    private void ResolutionHandler(ChangeEvent<string> evnt) {
        DropdownField temp = evnt.currentTarget as DropdownField;
        int width = int.Parse(temp.value.Split("x").First());
        int height = int.Parse(temp.value.Split("x").Last());
        foreach(Resolution r in Screen.resolutions) {
            if (r.height.Equals(height) && r.width.Equals(width)) Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        }
    }


    private void InitQualityDropdown(DropdownField input) {
        input.value = QualitySettings.names.GetValue(QualitySettings.GetQualityLevel()).ToString();
        input.choices = QualitySettings.names.ToList();
    }

    private void QualityHandler(ChangeEvent<string> evnt) {
        DropdownField temp = evnt.currentTarget as DropdownField;
        for(int i = 0; i < QualitySettings.names.Length; i++) {
            if (QualitySettings.names[i].Equals(temp.text)) QualitySettings.SetQualityLevel(i);
        }
    }

    private void FullscreenToggle(ChangeEvent<bool> evnt) {
        Screen.fullScreen = evnt.newValue;
    }

    private void SwitchPage(VisualElement oldPage, VisualElement newPage) {
        if(oldPage != null) oldPage.style.display = DisplayStyle.None;
        newPage.style.display = DisplayStyle.Flex;
    }

}
