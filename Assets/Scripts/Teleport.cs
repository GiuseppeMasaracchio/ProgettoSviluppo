
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject playerAsset;
    private RaycastHit raycastHit;
    //private Shader shader;

    void Awake() {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerAsset = GameObject.Find("PlayerAsset");
    }

    void Update() {
        //Debug.Log(player);    
    }

    
    private void OnTriggerEnter(Collider other) {
        //float angle;
        Physics.Raycast(playerRb.transform.position, playerAsset.transform.forward, out raycastHit, 2f);
        float RNAngle = Vector3.Angle(playerAsset.transform.right, raycastHit.normal);
        //float FNAngle = Vector3.Angle(playerAsset.transform.forward, raycastHit.normal);
        //Debug.Log(RNAngle);
        
        if (RNAngle < 90f) {
            Vector3 direction = playerAsset.transform.right + raycastHit.normal;
            playerRb.AddForce(direction * 10f, ForceMode.Impulse);
        } 
        else if (RNAngle > 90f) {
            Vector3 direction = -playerAsset.transform.right + raycastHit.normal;
            playerRb.AddForce(direction * 10f, ForceMode.Impulse);
        } 
        else {
            playerRb.AddForce(playerAsset.transform.forward * -20f, ForceMode.Impulse);
        }
        
        /*
        if (angle == 0f) {
            playerRb.AddForce(playerAsset.transform.forward * -15f, ForceMode.Impulse);
        } else {
            Vector3 direction = Mathf.Cos(angle + offset) * raycastHit.normal + Mathf.Sin(angle - offset) * -playerAsset.transform.forward;
            
            playerRb.AddForce(direction * 15f, ForceMode.Impulse);        
        }
        */
        //player.AddForce(playerAsset.transform.forward * -20f, ForceMode.Impulse);
        //player.position = checkpoint;
    }
    
}
