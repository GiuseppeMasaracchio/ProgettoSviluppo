using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class NewPause : MonoBehaviour
{
    private VisualElement root;

    //private Button[] _mainPageButtons;
    //private Button[] _settingsTabs;
    //private VisualElement[] _pages;
    //private VisualElement[] _settingsContent;

    private List<Button> _mainPageButtons;
    private List<Button> _settingsTabs;
    private Button _backButton;

    private List<VisualElement> _pages;
    private List<VisualElement> _settingsContent;

   

    private VisualElement currentContent = null;

    //Typewriter
    private string line = "";
    [SerializeField] private float charDelay = 0.02f;

    private void OnEnable() {
        
    }

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    // Start is called before the first frame update
    void Start() {
        InitPauseMenu();
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

    private void ButtonDispose(Button[] arr) {
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
        currentContent = _settingsContent.First();
    }

    private void OnAudioTab(ClickEvent evnt) {
        SwitchPage(currentContent, _settingsContent.ElementAt(1));
        currentContent = _settingsContent.ElementAt(1);
    }

    private void OnControlsTab(ClickEvent evnt) {
        SwitchPage(currentContent, _settingsContent.ElementAt(2));
        currentContent = _settingsContent.ElementAt(2);
    }

    private void InitPauseMenu() {
        _mainPageButtons = root.Query("MainPage").Children<Button>().ToList();
        ButtonHandlers(_mainPageButtons);

        _settingsTabs = root.Query("TabsContainer").Children<Button>().ToList();
        ButtonHandlers(_settingsTabs);

        _pages = root.Query("Body").Children<VisualElement>().ToList();

        _settingsContent = root.Query("ContentContainer").Children<VisualElement>().ToList();

        _backButton = root.Q("Back") as Button;
        _backButton.RegisterCallback<ClickEvent>(OnBackHandler);
    }

    private void SwitchPage(VisualElement oldPage, VisualElement newPage) {
        if(oldPage != null) oldPage.style.display = DisplayStyle.None;
        newPage.style.display = DisplayStyle.Flex;
    }

}
