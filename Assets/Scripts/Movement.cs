using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector2 direction = Vector2.zero;
    private Rigidbody player;
    private Vault vault;
    public bool jumpstate;
    public bool jumpcd = true;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        
    }

    public Vector3 Move() {
        return new Vector3(direction.x, 0f, direction.y);
    }

    public void Direction(Vector2 dir) {
        direction = dir;
    }

    public void Grounded() { 
        player.AddForce(Move().normalized * vault.Get("movespeed") * Time.fixedDeltaTime);
    
    }

    public void Airborne() {
        player.AddForce(Move().normalized * vault.Get("movespeed") * vault.Get("airborne") * Time.fixedDeltaTime);
    }

    public void Walk(bool grounded) {
        if (grounded) {
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

        //player.AddForce(Vector3.up * 1000f * Time.fixedDeltaTime, ForceMode.Impulse);
        Debug.Log("Jumping");
        Invoke(nameof(JumpCd), 1f);
    }
}
