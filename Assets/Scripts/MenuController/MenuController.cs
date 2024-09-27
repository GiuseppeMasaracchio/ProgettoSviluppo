using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour { 
    public MenuController Instance { get; private set; }

    [SerializeField] GameObject[] slots;
    [SerializeField] PlayerInfo _playerInfo;

    public UIMode mode = UIMode.Slots;

    private int currentSlot = 0;
    private int selectedSlot = 0;
    private int direction;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
               
    }

    private void Start() {
        StartCoroutine("DisplaySlots");
        SelectLights();

    }

    public void OnNavigate(InputValue input) {
        if (input.Get() == null) { return; }

        switch (mode) {
            case UIMode.MainMenu: {
                    //NavigateMenu()
                    break;
                }
            case UIMode.Slots: {
                    NavigateSlot(input.Get<Vector2>());
                    break;
                }
            case UIMode.Pause: {
                    //NavigatePause()
                    break;
                }

        }
        
    }

    public void OnSubmit(InputValue input) {
        if (input.Get() == null) { return; }

        switch (mode) {
            case UIMode.MainMenu: {
                    //SubmitMenu()
                    break;
                }
            case UIMode.Slots: {
                    SelectSlot();
                    break;
                }
            case UIMode.Pause: {
                    //SubmitPause()
                    break;
                }

        }
        
    }

    public void OnPause(InputValue input) {
        if (input.Get() == null) { return; }

        ScenesManager.Instance.MainMenu();
    }

    public void OnCancel(InputValue input) {
        if (input.Get() == null) { return; }
        
        ContinueGame();
    }

    public void ContinueGame() {
        DataManager.Instance.ResumeData();
        ScenesManager.Instance.LoadGame();
    }

    private void SelectSlot() {
        if (DataManager.Instance.GetSlotInfo(currentSlot).Checkpoint.x == 0) {
            DataManager.Instance.AssignSlotInfo((SaveSlot)currentSlot);
            ScenesManager.Instance.StartGame();
        } else {
            DataManager.Instance.AssignSlotInfo((SaveSlot)currentSlot);
            ScenesManager.Instance.LoadGame();
        }

    }

    private void NavigateSlot(Vector2 input) {
        if (input == new Vector2(1f, 0f)) {
            direction = 1;
            SwitchSlot(direction);
        }

        if (input == new Vector2(-1f, 0f)) {
            direction = 0;
            SwitchSlot(direction);
        }
    }

    private void SwitchSlot(int direction) {
        DeselectLights();
        
        if (direction == 1) {

            if (selectedSlot < (int)SaveSlot.Three) {
                selectedSlot++;
            } else {
                selectedSlot = (int)SaveSlot.One;
            }

        } else {

            if (selectedSlot > (int)SaveSlot.One) {
                selectedSlot--;
            }
            else {
                selectedSlot = (int)SaveSlot.Three;
            }

        }

        currentSlot = selectedSlot;
        SelectLights();
    }

    private void SelectLights() {        
        foreach (Light light in slots[selectedSlot].GetComponentsInChildren<Light>()) {
            if (light.tag == "Light") {
                light.enabled = true;
            } else {
                light.enabled = false;
            }
        }        
    }

    private void DeselectLights() {
        foreach (Light light in slots[currentSlot].GetComponentsInChildren<Light>()) {
            if (light.tag == "Light") {
                light.enabled = false;
            }
            else {
                light.enabled = true;
            }
        }
    }

    private IEnumerator DisplaySlots() {
        int i = 0;

        foreach (GameObject slot in slots) {

            if (DataManager.Instance.GetSlotInfo(i).Checkpoint.x != 0) {

                TMP_Text[] fields = slot.GetComponentsInChildren<TMP_Text>();

                fields[0].text = DataManager.Instance.GetSlotInfo(i).Name;
                fields[1].text = DataManager.Instance.GetSlotInfo(i).CurrentHp + " Hp";
                fields[2].text = "CP: " + DataManager.Instance.GetSlotInfo(i).Checkpoint.x + "-" + DataManager.Instance.GetSlotInfo(i).Checkpoint.y;

                i++;

            }
            
            yield return null;

        }        

        yield break;
    }

}
