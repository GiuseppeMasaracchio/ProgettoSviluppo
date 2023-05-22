using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approaching : MonoBehaviour
{
    private StateSetter state;
    private Rigidbody player;
    private MeshCollider cylinder;

    void Awake() {
        state = GameObject.Find("ScriptsHolder").GetComponent<StateSetter>();
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        cylinder = GameObject.Find("Approaching").GetComponent<MeshCollider>();
    }

    void Update() {
        if (player.velocity.y < -1f) { 
            cylinder.enabled = true; 
        } else {
            cylinder.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            state.SetApproaching(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        //state.SetApproaching(false);
    }
}
