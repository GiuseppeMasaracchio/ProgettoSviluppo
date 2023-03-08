using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private bool grounded;
    private Vault vault;
    private Transform player;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        player = GameObject.Find("Player").transform;
    }

    public void isGrounded() {
        grounded = Physics.Raycast(player.position, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        //Debug.Log(grounded);
        vault.SetGrounded(grounded);
        
    } 
}
