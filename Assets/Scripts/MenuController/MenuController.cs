using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour { 
    public MenuController Instance { get; private set; }

    [SerializeField] GameObject[] slots;
    [SerializeField] PlayerInfo _playerInfo;

    private List<object[]> saves; //Da implementare su DataManager
    private List<object[]> checkpoints; //Da implementare su DataManager

    private UIMode mode = UIMode.Slots;

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
        RetrieveSaves(); //Da implementare su DataManager

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
        
        //ScenesManager.Instance.QuitGame();
    }

    private void SelectSlot() {
        if ((int)checkpoints[currentSlot][1] == 0) {
            ScenesManager.Instance.StartGame();
        } else {
            AssignSlotInfo();
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

            if (selectedSlot < 2) {
                selectedSlot++;
            } else {
                selectedSlot = 0;
            }

        } else {

            if (selectedSlot > 0) {
                selectedSlot--;
            }
            else {
                selectedSlot = 2;
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

    private void RetrieveSaves() {  //Da implementare su DataManager
        /*
        object[] cpinfo = new object[2];
        cpinfo[0] = 2;
        cpinfo[1] = 0;

        DBVault.SetActiveSlot(currentSlot);
        DBVault.SetCheckpoint(cpinfo);
        */

        saves = DBVault.GetSlotsData();
        checkpoints = DBVault.GetSlotsCheckpoint();
    }

    private void AssignSlotInfo() {  //Da implementare su DataManager
        _playerInfo.CurrentHp = (int)saves[currentSlot][(int)ActiveData.CurrentHp];
        _playerInfo.PowerUps = (int)saves[currentSlot][(int)ActiveData.PowerUps];
        _playerInfo.Checkpoint = new Vector2((int)checkpoints[currentSlot][1], (int)checkpoints[currentSlot][2]);
    }

    private IEnumerator DisplaySlots() {
        int i = 0;

        foreach (GameObject slot in slots) {

            if ((int)checkpoints[i][1] != 0) {

                TMP_Text[] fields = slot.GetComponentsInChildren<TMP_Text>();

                fields[0].text = saves[i][(int)ActiveData.Name].ToString();
                fields[1].text = saves[i][(int)ActiveData.CurrentHp].ToString() + " Hp";
                fields[2].text = "CP: " + checkpoints[i][1].ToString() + "-" + checkpoints[i][2].ToString();
                i++;
                yield return null;

            }

        }        

        yield break;
    }

}
