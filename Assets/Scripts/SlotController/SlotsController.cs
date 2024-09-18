using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlotsController : MonoBehaviour { 
    [SerializeField] GameObject[] slots;

    private List<object[]> saves;
    private List<object[]> checkpoints;

    private object[] data = new object[4];
    private object[] cpinfo = new object[2];

    private int currentSlot = 0;
    private int selectedSlot = 0;
    private int direction;

    private void Awake() {       
        RetrieveSaves();        
    }

    private void Start() {
        /*
        DBVault.SetActiveSlot(selectedSlot);

        data[0] = "Puzzo"; //Name
        data[1] = 2; //PowerUps
        data[2] = 0; //Score
        data[3] = 3; //CurrentHp

        cpinfo[0] = 1;
        cpinfo[1] = 0;

        DBVault.SetCheckpoint(cpinfo);
        DBVault.UpdateActiveSlot(data);
        */

        StartCoroutine("DisplaySlots");
        SelectLights();
    }

    public void OnNavigate(InputValue input) {
        if (input.Get() == null) { return; }

        if (input.Get<Vector2>() == new Vector2(1f, 0f)) {
            direction = 1;
            SwitchSlot(direction);
        }

        if (input.Get<Vector2>() == new Vector2(-1f, 0f)) {
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

    private void RetrieveSaves() {        
        saves = DBVault.GetSlotsData();
        checkpoints = DBVault.GetSlotsCheckpoint();
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
