using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseTest : MonoBehaviour
{
    private VisualElement root;

    //Buttons
    private Button[] _mainPageButtons;
    private Button[] _settingsTabs;
    private Button[] _videoSetButtons;

    private Button currentTab = null;

    private Slider[] _audioSliders;

    //Pages
    private VisualElement _mainPage;
    private VisualElement _settingsPage;
    private VisualElement _choicesPage;


    //Typewriter
    private string line = "";
    [SerializeField] float charDelay = 0.2f;

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    // Start is called before the first frame update
    void Start()
    {
        PagesRef();
        InitAllButtons();
        StartCoroutine(CharAnim(_mainPageButtons));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitAllButtons() {
        _mainPageButtons = InitButtons("MainPage").ToArray();
        ButtonDispatcher(_mainPageButtons);

        _settingsTabs = InitButtons("TabsContainer").ToArray();
        ButtonDispatcher(_settingsTabs);

        _videoSetButtons = InitButtons("VideoSettings").ToArray();
        ButtonDispatcher(_videoSetButtons);

        _audioSliders = InitSliders().ToArray();
    }

    private void PagesRef() {
        _mainPage = root.Q("MainPage");
        _settingsPage = root.Q("SettingsPage");
        _choicesPage = root.Q("ChoicesContainer");
    }

    private List<Slider> InitSliders() {
        return root.Query("AudioSettings").Children<Slider>().ToList();
    }
    private List<Button> InitButtons(string query) {
        return root.Query(query).Children<Button>().ToList();
    }

    private IEnumerator CharAnim(Button[] arr) {
        foreach(Button b in arr) {
            line = b.text;
            b.text = "";
            b.style.opacity = 100f;

            foreach (char c in line) {
                b.text += c;
                yield return new WaitForSeconds(charDelay);
            }
            yield return new WaitUntil(() => line.Length == b.text.Length);
            line = "";
        }
            yield break;
    }

    private void ButtonDispatcher(Button[] arr) {
        switch (arr[0].parent.name) {
            case "MainPage":
                foreach (Button b in arr) b.RegisterCallback<ClickEvent>(OnMainPageButton);
                break;

            case "TabsContainer":
                foreach (Button b in arr) b.RegisterCallback<ClickEvent>(OnSettingsTab);
                break;

            case "VideoSettings":
                foreach (Button b in arr) {
                    b.RegisterCallback<ClickEvent>(OnVideoSettings);
                    Label l = b.ElementAt(0) as Label;
                    SetVideoValues(l);
                }
                break;
        }
    }

    private void ButtonSdispatcher(Button[] arr) {
        switch (arr[0].parent.name) {
            case "MainPage":
                foreach (Button b in arr) b.UnregisterCallback<ClickEvent>(OnMainPageButton);
                break;

            case "TabsContainer":
                foreach (Button b in arr) b.UnregisterCallback<ClickEvent>(OnSettingsTab);
                break;

            case "VideoSettings":
                foreach (Button b in arr)   b.UnregisterCallback<ClickEvent>(OnVideoSettings);
                break;
        }
    }

    private void OnMainPageButton(ClickEvent evnt) {
        VisualElement temp = evnt.currentTarget as VisualElement;
        switch (temp.name) {
            case "Resume":
                gameObject.SetActive(false);
                DisposeAllButtons();
                break;
            case "Settings":
                _mainPage.style.display = DisplayStyle.None;
                _settingsPage.style.display = DisplayStyle.Flex;
                StartCoroutine(CharAnim(_settingsTabs));
                break;
            case "MainMenu":
                //SceneManager.Switch("MainMenu");  ??
                break;
        }
    }

    private void OnSettingsTab(ClickEvent evnt) {
        Button temp = evnt.currentTarget as Button;

        switch (temp.name) {
            case "VideoTab":
                StartCoroutine(CharAnim(_videoSetButtons));

                _videoSetButtons[0].parent.style.display = DisplayStyle.Flex;   //provvisorio
                break;
            case "AudioTab":
                Debug.Log("Premuta tab: " + temp.name);

                break;
            case "ControlsTab":
                Debug.Log("Premuta tab: " + temp.name);
                break;
        }

        if (currentTab == null) {
            currentTab = temp;
            currentTab.AddToClassList("Selected");
        }

        if (!temp.ClassListContains("Selected") && !temp.Equals(currentTab)) {
            temp.AddToClassList("Selected");
            currentTab.RemoveFromClassList("Selected");
            currentTab = temp;
        }
    }   
    
    //Dev'essere chiamata anche da fuori
    public void SetVideoValues(Label l) {
        switch (l.parent.name) {
            case "Resolution":
                string temp = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
                l.text = temp;
                break;
            case "Quality":
               l.text = QualitySettings.GetQualityLevel().ToString();
                break;
            case "Fullscreen":
                l.text = (Screen.fullScreen) ? "Enabled" : "Disabled";
                break;
        }
    }

    private void OnVideoSettings(ClickEvent evnt) {
        VisualElement temp = evnt.currentTarget as VisualElement;
        switch (temp.name) {
            case "Resolution":
                Debug.Log("NomeBottone: " + temp.name);
                break;
            case "Quality":
                Debug.Log("Nome: " + temp.name);
                break;
            case "Fullscreen":
                Screen.fullScreen = !Screen.fullScreen;
                break;
        }
    }

    private void DisposeAllButtons() {
        ButtonSdispatcher(_mainPageButtons);
        ButtonSdispatcher(_settingsTabs);
        ButtonSdispatcher(_videoSetButtons);
    }
}
