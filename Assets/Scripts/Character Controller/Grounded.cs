using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To be renamed MorePhysics
public class Grounded : MonoBehaviour
{
    private bool grounded;
    private float acceleration;

    private Vault vault;
    private GameObject player;
    private Rigidbody rb;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    public void SetGround() {
        grounded = Physics.Raycast(player.transform.position, Vector3.down, player.transform.localScale.y / 2 + 0.3f, LayerMask.GetMask("Ground"));
        vault.SetGrounded(grounded);
        
    } 

    private float SetAcceleration() {
        if (vault.GetGrounded()) {
            acceleration = 0f;
            return acceleration; 
        } else {
            acceleration += -9.81f * 4f * Time.deltaTime;
            return acceleration;
        }
    }

    public void AdditionalGravity() {
        if (rb.velocity.y > 0f) {
            return; 
        } else {
            rb.AddForce(Vector3.up * SetAcceleration(), ForceMode.Acceleration);
            return;
        }

    }
}
