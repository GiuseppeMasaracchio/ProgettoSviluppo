using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    //private Vault vault;
    private StateSetter state;
    private Rigidbody player;

    void Awake() {
        //vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        state = GameObject.Find("ScriptsHolder").GetComponent<StateSetter>();
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
    }



    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            Vault.SetGrounded(true);
            state.SetApproaching(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            Vault.SetGrounded(false);
        }
    }
}
