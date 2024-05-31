using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
public class NewBehaviourScript : MonoBehaviour
{
    private List<VisualElement> _pausePages;


    VisualElement[] mainPageButtons, tabsContainer, settingsContainer, choicesContainer;
    VisualElement currentTab;

    string line;
    [SerializeField] private float charDelay = 0.2f;
    string currentRes = "1920x1080";


    private void Awake() {
        VisualElement temp = GetComponent<UIDocument>().rootVisualElement.Query("PageContainer");
        _pausePages = temp.Children().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        //choicesContainer = GetComponent<UIDocument>().rootVisualElement.Query("ChoicesContainer");
        //SetTypewriter(mainPageButtons);

        InitAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitAll() {
        InitializeAllPauseButtons();
        InitializeMainPage();
        InitializeSettingsPage();
        InitializeVideoSettings();
        InitVideoSettings();
    }

    private void SetTypewriter(VisualElement[] arr) {
        foreach(Button b in arr) {
            line = b.text;
            StartCoroutine(Typewriter());
        }
    }

    private IEnumerator Typewriter() {
        string newLine = line;
        line = "";
        foreach (char c in newLine) {
            line += c;
            yield return new WaitForSeconds(charDelay);
        }
        yield return new WaitUntil(() => line.Length == newLine.Length);

        yield break;
    }

    private void InitializeAllPauseButtons() {
        mainPageButtons = _pausePages[0].Children().ToArray();
        VisualElement[] settingsPage = _pausePages[1].Children().ToArray();

        tabsContainer = settingsPage[0].Children().ToArray();
        settingsPage = settingsPage[1].Children().ToArray();
    }

    private void InitializeMainPage() {
        mainPageButtons = _pausePages[0].Children().ToArray();

        foreach(Button b in mainPageButtons) {
            b.RegisterCallback<ClickEvent>(
                (ClickEvent evnt) => {

                if(evnt.currentTarget.Equals(mainPageButtons[0])) { //resume
                    //To do !isPaused -> Time.TimeScale = 1f;
                }

                if(evnt.currentTarget.Equals(mainPageButtons[1])) { //Settings
                    _pausePages[0].style.display = DisplayStyle.None;
                    _pausePages[1].style.display = DisplayStyle.Flex;
                        SetTypewriter(tabsContainer);
                        SetTypewriter(settingsContainer);


                        //Turbo temporaneo
                        foreach (VisualElement elem in settingsContainer) elem.style.display = DisplayStyle.None;
                }

                if(evnt.currentTarget.Equals(mainPageButtons[2])) { //Main Menu
                    //SceneManager.LoadScene("mainMenu");
                }
            });
        }
    }

    private void InitializeSettingsPage() {

        foreach(Button b in tabsContainer) {
            b.RegisterCallback<ClickEvent>( 
                (ClickEvent evnt) => {

                if(evnt.currentTarget.Equals(tabsContainer[0])) currentTab = settingsContainer[0];
                
                if (evnt.currentTarget.Equals(tabsContainer[1])) currentTab = settingsContainer[1];

                if (evnt.currentTarget.Equals(tabsContainer[2])) currentTab = settingsContainer[2];
            });
        }
        currentTab.style.display = DisplayStyle.Flex;
    }

    private void InitializeVideoSettings() {
        VisualElement[] videoSettingsButtons = settingsContainer[0].Children().ToArray();
        //List<Label> videoValues = new List<Label>();
        Label[] values = new Label[2];
        values[0].text = currentRes;

        foreach (Button b in videoSettingsButtons) {
            //videoValues.Add(b.GetFirstOfType<Label>());

            values.Append(b.GetFirstOfType<Label>());
            values.Last().text = Screen.fullScreen.ToString();

            b.RegisterCallback<ClickEvent>(
                (ClickEvent evnt) => {

                    if (evnt.currentTarget.Equals(videoSettingsButtons[0])) { //Resolution
                        choicesContainer[0].style.display = DisplayStyle.Flex;
                    }

                    if (evnt.currentTarget.Equals(videoSettingsButtons[1])) { //Quality
                        choicesContainer[1].style.display = DisplayStyle.Flex;
                    }

                    if (evnt.currentTarget.Equals(videoSettingsButtons[2])) { //Fullscreen
                        Screen.fullScreen = !Screen.fullScreen; 
                        //Screen.fullScreen = values[2].Equals("Enabled");
                    }
                });
        }

    }

    private void InitVideoSettings() {
        Resolution[] res = Screen.resolutions;
        foreach(Resolution r in res) {
            Button temp = new Button();
            temp.text = r.height + "x" + r.width;
            temp.RegisterCallback<ClickEvent>((ClickEvent evnt) => {
                //currentRes = "";
                currentRes = temp.text;
            });
            choicesContainer[0].Add(temp);
        }
    }
}
