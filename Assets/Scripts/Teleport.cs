
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Rigidbody player;
    private GameObject playerAsset;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerAsset = GameObject.Find("PlayerAsset");
    }

    void Update() {
        //Debug.Log(player);    
    }

    
    private void OnTriggerEnter(Collider other) {
        player.AddForce(playerAsset.transform.forward * -20f, ForceMode.Impulse);
        //player.position = checkpoint;
    }
 
}
