using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector2 input = Vector2.zero;
    private Rigidbody player;
    private Transform orientation;
    private Vector3 direction;

    private Vault vault;
    public bool jumpstate;
    public bool jumpcd = true;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        orientation = GameObject.Find("Player").transform;
    }

    public Vector3 Move() {
        direction = orientation.forward * input.y + orientation.right * input.x;
        return direction;
        //return new Vector3(movement.forward * direction.x, 0f, direction.y);
    }

    public void Direction(Vector2 dir) {
        input = dir;
    }

    public void Grounded() { 
        player.AddForce(Move() * vault.Get("movespeed") * Time.deltaTime);
    
    }

    public void Airborne() {
        player.AddForce(Move() * vault.Get("movespeed") * vault.Get("airborne") * Time.deltaTime);
    }

    public void Walk() {
        if (vault.GetGrounded()) {
            Grounded();
        } else {
            Airborne();
        }
    }

    //Jump related methods

    public void JumpCd() {
        jumpcd = false;

    }

    public void JumpInput(float input) {
        if (input.Equals(1f)) {
            jumpstate = true;
            jumpcd = false;

        } else if (input.Equals(0f)) {
            jumpstate = false;
            jumpcd = true;
        } 
    }

    public void GroundJumpCheck() {
        if (!vault.GetGrounded()) {
            return;
        }
        else {
            JumpCheck();
        }
    }

    public void JumpCheck() {
        if (!jumpstate) { 
            return; 
        } else {
            Jump();
        }
    }

    public void Jump() {
        if (jumpcd) { return; }

        jumpcd = true;
        player.velocity.Set(player.velocity.x, 0f, player.velocity.z);
        player.AddForce(Vector3.up * 1000f * Time.fixedDeltaTime, ForceMode.Impulse);
        Debug.Log("Jumping");
        Invoke(nameof(JumpCd), 2f);
    }
}
