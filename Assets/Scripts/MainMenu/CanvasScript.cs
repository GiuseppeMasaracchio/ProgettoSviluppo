using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Button _currentTab;

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
