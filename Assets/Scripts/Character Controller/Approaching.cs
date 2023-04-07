using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approaching : MonoBehaviour
{
    private StateSetter state;

    void Awake() {
        state = GameObject.Find("ScriptsHolder").GetComponent<StateSetter>();
    }



    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            state.SetApproaching(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        state.SetApproaching(false);
    }
}
