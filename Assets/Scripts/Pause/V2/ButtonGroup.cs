using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonGroup : MonoBehaviour {

    private TMP_Text titleText;

    private bool show = false;
    [SerializeField] private float flashStart = 0.3f, flashAnim = 1f;

    [SerializeField] private List<Button> buttonsList = new List<Button>();

    private void Awake() {
    }

    private void Start() {
        titleText = GetComponentInChildren<TMP_Text>();
    }

    private void AddButtons() {
        Button[] collection = GetComponentsInChildren<Button>(true);
        if(collection != null)
            foreach(Button b in collection) {
                buttonsList.Add(b);
            }
    }

    private void Update() {
        titleText.alpha = show ? 200 : 0 ;
    }

    private void OnEnable() {
        InvokeRepeating("Flash", flashStart, flashAnim);
    }
    private void Flash() {
        show = !show;
    }
}
