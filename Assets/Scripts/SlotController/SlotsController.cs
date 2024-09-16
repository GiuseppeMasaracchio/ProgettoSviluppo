using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotsController : MonoBehaviour {
    [SerializeField] GameObject slot1;
    [SerializeField] GameObject slot2;
    [SerializeField] GameObject slot3;

    private List<object[]> saves;
    private List<object[]> checkpoints;

    private GameObject[] slots = new GameObject[3];

    //private int selectedSlot = 1;

    private void Awake() {       
        InitializeSlots();
        RetrieveSaves();
    }

    private void Start() {
        StartCoroutine("DisplaySlots");
    }

    private void InitializeSlots() {
        slots[0] = slot1;
        slots[1] = slot2;
        slots[2] = slot3;
    }

    private void RetrieveSaves() {        
        saves = DBVault.GetSlotData();
        checkpoints = DBVault.GetSlotCheckpoints();
    }

    private IEnumerator DisplaySlots() {
        int i = 0;

        foreach (GameObject slot in slots) {
            TMP_Text[] fields = slot.GetComponentsInChildren<TMP_Text>();

            fields[0].text = "Slot: " + saves[i][(int)ActiveData.Slot_ID].ToString();
            fields[1].text = "Hp: " + saves[i][(int)ActiveData.CurrentHp].ToString();
            fields[2].text = "CP: " + checkpoints[i][1].ToString() + "-" + checkpoints[i][2].ToString();
            i++;
            yield return null;
            
        }        

        yield break;
    }

}
