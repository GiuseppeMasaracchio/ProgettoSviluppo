using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMScript : MonoBehaviour
{
    private VisualElement root;

    private Label _header;

    //Bottoni vari
    private List<UnityEngine.UIElements.Button> _mainPageButtons = new List<UnityEngine.UIElements.Button>();
    private List<UnityEngine.UIElements.Button> _saveslots = new List<UnityEngine.UIElements.Button>();
    private List<UnityEngine.UIElements.Button> _settingsTabs = new List<UnityEngine.UIElements.Button>();
    private List<UnityEngine.UIElements.Button> _backButtons = new List<UnityEngine.UIElements.Button>();
    //private List<UnityEngine.UIElements.Button> _videoSettings = new List<UnityEngine.UIElements.Button>();
    private List<UnityEngine.UIElements.Button> _audioSettings = new List<UnityEngine.UIElements.Button>();
    private List<VisualElement> _videoSettings = new List<VisualElement>();
    //Pagine
    private VisualElement _settingsPage;
    private VisualElement _saveslotsPage;

    //Content
    private List<VisualElement> _settingsContent;

    //Typewriter
    private string newLine = "";
    [SerializeField] private float typewriterDelay;

    VisualElement currentContent;
    List<UnityEngine.UIElements.Button> currentList = new List<UnityEngine.UIElements.Button>();


    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    // Start is called before the first frame update
    void Start() {
        //riferimento bottoni vari
        _mainPageButtons = root.Query("MainPage").Children<UnityEngine.UIElements.Button>().ToList();
        _saveslots = root.Query("SaveslotsContainer").Children<UnityEngine.UIElements.Button>().ToList();
        _settingsTabs = root.Query("TabsContainer").Children<UnityEngine.UIElements.Button>().ToList();
        _backButtons = root.Query<UnityEngine.UIElements.Button>("BackButton").ToList();


        //settings
        //_videoSettings = root.Query("VideoContent").Children<UnityEngine.UIElements.Button>().ToList();
        _videoSettings = root.Query("VideoContent").Children<VisualElement>().ToList();
        _audioSettings = root.Query("AudioContent").Children<UnityEngine.UIElements.Button>().ToList();

        //riferimento pagine
        _settingsPage = root.Q("SettingsPage");
        _saveslotsPage = root.Q("SaveslotsPage");

        //riferimento settings contents
        _settingsContent = root.Query("ContentContainer").Children<VisualElement>().ToList();
        
        _header = root.Q("Header") as Label;

        newLine = "Main Menu";
        StartCoroutine(Typewriter());

        currentList = _mainPageButtons;
        StartCoroutine(LabelsTextAnim());

        InitializeAll();
    }
    private void InitializeAll() {
        InitializeMainPageButtons();
        InitializeBackButtons();
        InitializeSaveslots();
        InitializeSettingsTabs();
    }

    private IEnumerator LabelsTextAnim() {
        foreach (UnityEngine.UIElements.Button b in currentList) {
            newLine = b.text;
            b.text = "";
            foreach (char c in newLine) {
                b.text += c;
                yield return new WaitForSeconds(typewriterDelay);
            }
            yield return new WaitUntil(() => b.text.Length == newLine.Length);
            newLine = "";
        }
        yield break;
    }


    private IEnumerator Typewriter() {
        _header.text = "";
        foreach(char c in newLine) {
            _header.text += c;
            yield return new WaitForSeconds(typewriterDelay);
        }
        yield return new WaitUntil( () => _header.text.Length == newLine.Length);
        newLine = "";
        yield break;
    }

    private void InitializeMainPageButtons() {
        foreach(UnityEngine.UIElements.Button b in _mainPageButtons) {
            b.RegisterCallback<ClickEvent>(MainPageButtonsHandler);
        }
    }

    private void InitializeSettingsTabs() {
        foreach(UnityEngine.UIElements.Button b in _settingsTabs) {
            b.RegisterCallback<ClickEvent>(SettingsPageHandler);
        }
    }

    
    private void InitializeBackButtons() {
        foreach(UnityEngine.UIElements.Button b in _backButtons) {
            b.RegisterCallback<ClickEvent>( (ClickEvent evnt)=> {
                b.parent.parent.style.display = DisplayStyle.None;
                newLine = "Main Menu";
                StartCoroutine(Typewriter());
                _mainPageButtons[0].parent.style.display = DisplayStyle.Flex;
            });
        }
    }

    //temp
    private void InitializeSaveslots() {
        foreach (UnityEngine.UIElements.Button s in _saveslots)
            s.RegisterCallback<ClickEvent>( (ClickEvent evnt) => {
                SceneManager.LoadScene(0);
            });
    }


   private void InitializeVideoSettings() {
        DropdownField temp = (DropdownField)_videoSettings[0];
        foreach(Resolution res in Screen.resolutions) {
            temp.choices.Add(res.ToString());
        }

        temp = (DropdownField)_videoSettings[1];
        //QualitySettings
   }



    private void MainPageButtonsHandler(ClickEvent evnt) {
        string buttonName = "";
        foreach(UnityEngine.UIElements.Button b in _mainPageButtons) {
            if (b.Equals(evnt.currentTarget)) buttonName = b.name;
        }

        switch (buttonName) {
            case "NewGameButton":
                _mainPageButtons[0].parent.style.display = DisplayStyle.None;
                newLine = "Saveslots";
                StartCoroutine(Typewriter());

                _saveslotsPage.style.display = DisplayStyle.Flex;
                currentList = _saveslots;
                StartCoroutine(LabelsTextAnim());

                break;

            case "ContinueButton":
                _mainPageButtons[0].parent.style.display = DisplayStyle.None;
                newLine = "Saveslots";
                StartCoroutine(Typewriter());

                _saveslotsPage.style.display = DisplayStyle.Flex;
                currentList = _saveslots;
                StartCoroutine(LabelsTextAnim());

                break;

            case "SettingsButton":
                _mainPageButtons[0].parent.style.display = DisplayStyle.None;
                newLine = "Settings";
                StartCoroutine(Typewriter());
                _settingsPage.style.display = DisplayStyle.Flex;

                currentList = _settingsTabs;
                StartCoroutine(LabelsTextAnim());

                _settingsTabs[0].Focus();
                currentContent = _settingsContent[0];
                currentContent.style.display = DisplayStyle.Flex;
                break;

            case "QuitButton":
                Application.Quit();
                break;
        }
    }
   

    private void SettingsPageHandler(ClickEvent evnt) {
        //Provvisorio(?)
        int i = 0;

        foreach(UnityEngine.UIElements.Button tab in _settingsTabs) {
            if(tab.Equals(evnt.currentTarget))
            switch (tab.name) {
                case "VideoTab":
                    i = 0;
                    break;
                case "AudioTab":
                    i = 1;
                    break;
                case "ControlsTab":
                    i = 2;
                    break;
            }
        }

        currentContent.style.display = DisplayStyle.None;
        _settingsContent[i].style.display = DisplayStyle.Flex;
        currentContent = _settingsContent[i];
    }


    //To do videoSettings 
    private void VideoContentCallbacks() {
        _videoSettings[0].RegisterCallback<ChangeEvent<Resolution>>((ChangeEvent<Resolution> evnt) => {
            //set resolution
            Screen.SetResolution(evnt.newValue.width, evnt.newValue.height, true);
        });

        _videoSettings[1].RegisterCallback<ChangeEvent<int>>((ChangeEvent<int> evnt) => {
            //set quality
            QualitySettings.SetQualityLevel(evnt.newValue);
        });

        _videoSettings[2].RegisterCallback<ChangeEvent<bool>>((ChangeEvent<bool> evnt) => {
            Screen.fullScreen = evnt.newValue;
        });
    }

    //To do audioSettings 
    private void AudioSettingsHandler(ChangeEvent<string> evnt) {
        //
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
