using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class Movement : MonoBehaviour {
    private Vector2 direction = Vector2.zero;
    private Rigidbody player;
    private Vault vault;

    private bool canJump = true;
    private float jumpCd = 0.25f; 

    

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }
    public Vector3 Move() {
        
        return new Vector3(direction.x, 0f, direction.y);
    }

    public void Direction(Vector2 dir) {
        this.direction = dir;
    }


    public void Jump() {
        if (canJump) {
            canJump = false;

            player.velocity = new Vector3(player.velocity.x, 0f, player.velocity.z);
            player.AddForce(Vector3.up * vault.Get("jumpHeight"), ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCd);
        }
        
    }

    private void ResetJump() {
        canJump = true;
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
}
