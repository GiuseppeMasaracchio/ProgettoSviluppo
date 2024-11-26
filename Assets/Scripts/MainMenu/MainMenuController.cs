using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class MainMenuController : MonoBehaviour
{
    [System.Serializable]
    public enum boxName {
        mainBox,
        pauseBox,
        tabsBox,
        videoBox,
        audioBox,
        controlsBox,
        accessBox,
        credits
    }

    boxName currentBox;
    public static MainMenuController Instance { get; private set; }

    private InputAction _pointAction;
    private InputAction _navigateAction;
    private InputAction _quitAction;
    //To do: subscribe alle InputAction già presenti in MenuController

    [SerializeField] GameObject[] _boxes;

    private Button[] _mainButtons;
    private Button[] _pauseButtons;
    private Button[] _tabs;
    private Selectable[] _videoSettings;
    private Slider[] _audioSettings;

    [Header("VideoSettings")]
    [SerializeField] Dropdown _resDropdown;
    [SerializeField] Dropdown _qualityDropdown;
    [SerializeField] Toggle _fullscreen;

    private Button _currentTab;
    private bool isMainMenu;

    private void Awake() {
        if (Instance != this) Destroy(Instance);
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        //_pointAction = InputManager.Instance.GetPlayerInput().actions["Point"];

        currentBox = isMainMenu ? boxName.mainBox : boxName.pauseBox;

        InitializeActions();
        SubscribeCallbacks();

        FillArrays();
        SelectFirst(currentBox);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeActions() {
        _navigateAction = InputManager.Instance.GetPlayerInput().actions["Navigate"];
        _quitAction = InputManager.Instance.GetPlayerInput().actions["Quit"];
    }

    private void SubscribeCallbacks() {
        _navigateAction.performed += OnNavigate;
        _quitAction.started += OnQuit;
    }

    public void Test() {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    //private void InitVideoSettings() {
    //    DropdownField _resDropdown = _videoSettings[0].GetComponent<DropdownField>();
    //    DropdownField _qualityDropdown = _videoSettings[1].GetComponent<DropdownField>();
    //    UnityEngine.UI.Toggle _fullscreen = _videoSettings[2].GetComponent<UnityEngine.UI.Toggle>();

    //    foreach (Resolution r in Screen.resolutions) {
    //        _resDropdown.choices.Add(r.width + "x" + r.height);
    //    }
    //    _resDropdown.value = _resDropdown.choices.Last();

    //    _qualityDropdown.choices = QualitySettings.names.ToList();
    //    _qualityDropdown.value = _qualityDropdown.choices.Last();

    //    _fullscreen.value = Screen.fullScreen;
    //}

    //private void InitAudioSettings() {
    //    float x;
    //    //AudioMixer _mixer to do
    //    _mixer.GetFloat("MasterVolume", out x);
    //    _audioSettings[0].value = x;

    //    _mixer.GetFloat("MusicVolume", out x);
    //    _audioSettings[2].value = x;

    //    _mixer.GetFloat("EffectsVolume", out x);
    //    _audioSettings[3].value = x;
    //}

    private void TabSelect(Button tab) {
        if (_currentTab == tab) return;
        if(_currentTab != null) _currentTab.interactable = true;

        tab.interactable = false;
        _currentTab = tab;
    }
    private void SelectFirst(boxName newBox) {
        switch (newBox) {
            case boxName.mainBox:
                EventSystem.current.firstSelectedGameObject = _mainButtons[0].gameObject;
                break;
            case boxName.pauseBox:
                EventSystem.current.firstSelectedGameObject = _pauseButtons[0].gameObject;
                break;
            case boxName.tabsBox:
                EventSystem.current.firstSelectedGameObject = _tabs[0].gameObject;
                break;
            case boxName.videoBox:
                EventSystem.current.firstSelectedGameObject = _videoSettings[0].gameObject;
                break;
            case boxName.audioBox:
                EventSystem.current.firstSelectedGameObject = _audioSettings[0].gameObject;
                break;
        }
    }

    public void SwapperWrapper(int val) {
        SwitchBox((boxName)val);
    }

    private void SwitchBox(boxName newBox) {
        if (currentBox == newBox) return;
        if (currentBox != boxName.tabsBox) _boxes[(int)currentBox].SetActive(false);
        if ((int)newBox > 2) TabSelect(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());

        _boxes[(int)newBox].SetActive(true);
        SelectFirst(newBox);
        currentBox = newBox;
    }
    private void FillArrays() {
        _mainButtons = _boxes[(int)boxName.mainBox].GetComponentsInChildren<Button>();
        _pauseButtons = _boxes[(int)boxName.pauseBox].GetComponentsInChildren<Button>();
        _tabs = _boxes[(int)boxName.tabsBox].GetComponentsInChildren<Button>();
        _videoSettings = _boxes[(int)boxName.videoBox].GetComponentsInChildren<Selectable>();
        _audioSettings = _boxes[(int)boxName.audioBox].GetComponentsInChildren<Slider>();
    }

    public void ContinueButton() {
        MenuController.Instance.ContinueGame();
    }

    public void StartButton() {
        //MenuController.Instance.SubmitMenu();
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void ResumeButton() { 
        //Time.timeScale = 1f;
    }

    public void MainMenuButton() {
        MenuController.Instance.ReturnToMainMenu();
    }

    public void OnNavigate(InputAction.CallbackContext input) {
        Vector2 temp = input.ReadValue<Vector2>();
        int i = 0;
        i = (temp.y > 0) ? i++ : i--;
        switch (currentBox) {
            case boxName.mainBox:
                if (i < 0) i = _mainButtons.Length;
                _mainButtons[i].Select();
                break;

            case boxName.pauseBox:
                if (i < 0) i = _pauseButtons.Length;
                break;

            case boxName.tabsBox:
                if (i < 0) i = _tabs.Length;
                _tabs[i].Select();
                break;

            case boxName.videoBox:
                if (i < 0) i = _videoSettings.Length;
                _videoSettings[i].Select();
                break;

            case boxName.audioBox:
                if (i < 0) i = _audioSettings.Length;
                _audioSettings[i].Select();
                break;
        }
    }

    public void OnQuit(InputAction.CallbackContext input) {
        //if (input.phase != InputActionPhase.Started) return;

        if (currentBox == boxName.mainBox) ScenesManager.Instance.QuitGame();

        if (currentBox == boxName.pauseBox) return; //to do Resume

        if (currentBox == boxName.tabsBox) {
            if(isMainMenu) SwitchBox(boxName.mainBox);

            if (!isMainMenu) SwitchBox(boxName.pauseBox);
        }
        else SwitchBox(boxName.tabsBox);
        
    }

    
    #region AudioSettings
    public void SetMasterVolume(float val) {
        //_mixer.SetFloat("Master", val);
    }
    public void SetMusicVolume(float val) {
        //_mixer.SetFloat("Music", val);
    }
    public void SetEffectsVolume(float val) {
        //_mixer.SetFloat("Effects", val);
    }
    #endregion

    #region VideoSettings
    public void SetResolution(string input) {
        foreach (Resolution r in Screen.resolutions) {
            if (r.ToString().Equals(input)) Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        }
    }
    public void SetQuality(int input) {
        QualitySettings.SetQualityLevel(input);
    }

    public void SetFullscreen(bool input) {
        Screen.fullScreen = input;
    }
    #endregion
}
