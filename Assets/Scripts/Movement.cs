using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class Movement : MonoBehaviour {
    private Vector2 direction = Vector2.zero;
    private Rigidbody player;
    private Vault vault;

    private bool jumpCd = true;

    

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


    public async void Jump() {
        if (jumpCd) {
            jumpCd = false;
            player.AddForce(0f, 10f, 0f, ForceMode.Impulse);
            await Task.Delay(1000); //cd 1s
            jumpCd = true;
        }
        
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
