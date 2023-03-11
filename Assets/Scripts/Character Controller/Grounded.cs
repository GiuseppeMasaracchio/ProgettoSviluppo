using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private bool grounded;
    private Vault vault;
    private GameObject player;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        player = GameObject.Find("Player");
    }

    public void SetGround() {
        grounded = Physics.Raycast(player.transform.position, Vector3.down, player.transform.localScale.y / 2 + 0.1f, LayerMask.GetMask("Ground"));
        vault.SetGrounded(grounded);
        
    } 
}
