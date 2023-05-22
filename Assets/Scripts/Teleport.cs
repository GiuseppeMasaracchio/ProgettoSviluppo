
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Rigidbody player;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Update() {
        //Debug.Log(player);    
    }

    private void OnTriggerEnter(Collider other) {
        player.AddForce(player.transform.forward * -25f, ForceMode.Acceleration);
        //player.position = checkpoint;
    }
}
