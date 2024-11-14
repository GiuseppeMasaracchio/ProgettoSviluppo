using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public enum boxName {
        mainBox,
        TabsBox,
        videoBox,
        audioBox
    }

    boxName currentBox;
    public static MainMenuController Instance { get; private set; }

    private InputAction _pointAction;
    //To do: subscribe alle InputAction già presenti in MenuController

    [SerializeField] GameObject[] _boxes;

    private Button[] _mainButtons;
    private Button[] _tabs;
    private Selectable[] _videoSettings;
    private Selectable[] _audioSettings;


    // Start is called before the first frame update
    void Start()
    {
        //_pointAction = InputManager.Instance.GetPlayerInput().actions["Point"];

        currentBox = boxName.mainBox;
        FillArrays();
        SelectFirst(currentBox);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test() {
        Debug.Log(gameObject.name);
    }

    private void SelectFirst(boxName newBox) {
        switch (newBox) {
            case boxName.mainBox:
                EventSystem.current.firstSelectedGameObject = _mainButtons[0].gameObject;
                break;
            case boxName.TabsBox:
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

    public void SwitchBox(boxName newBox) {
        if (currentBox == newBox) return;
        if (currentBox != boxName.TabsBox) _boxes[(int)currentBox].SetActive(false);
        
        _boxes[(int)newBox].SetActive(true);
        SelectFirst(newBox);
        currentBox = newBox;
    }
    private void FillArrays() {
        _mainButtons = _boxes[(int)boxName.mainBox].GetComponentsInChildren<Button>();
        _tabs = _boxes[(int)boxName.TabsBox].GetComponentsInChildren<Button>();
        _videoSettings = _boxes[(int)boxName.videoBox].GetComponentsInChildren<Selectable>();
        _audioSettings = _boxes[(int)boxName.audioBox].GetComponentsInChildren<Selectable>();
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

            case boxName.TabsBox:
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
        if (input.phase != InputActionPhase.Started) return;

        if (currentBox == boxName.mainBox) ScenesManager.Instance.QuitGame();

        if (currentBox == boxName.TabsBox) {
            SwitchBox(boxName.mainBox);
        }
        else SwitchBox(boxName.TabsBox);
        
    }

    //public void OnPoint(InputAction.CallbackContext input) {
    //    Vector2 temp = input.ReadValue<Vector2>();
    //    Vector2 pos = _boxes[(int)currentBox].GetComponentInChildren<Selectable>().transform.position;
    //    Cursor.SetCursor(null, pos, CursorMode.Auto);
        
    //    if(input.phase == InputActionPhase.Performed) {

    //     if(temp.y > 0.4f ) {

    //        }   
    //    }
    //}
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
