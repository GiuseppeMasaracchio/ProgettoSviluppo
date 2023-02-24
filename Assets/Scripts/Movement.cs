using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Vector2 direction = Vector2.zero;
    [SerializeField] Rigidbody player;
    [SerializeField] float movespeed;
    [SerializeField] float airborne;

    public Vector3 Move() {
        return new Vector3(direction.x, 0f, direction.y);
    }

    public void Direction(Vector2 dir) {
        this.direction = dir;
    }

    public void Grounded() { 
        player.AddForce(Move().normalized * movespeed * Time.fixedDeltaTime);
    
    }

    public void Airborne() {
        player.AddForce(Move().normalized * movespeed * airborne * Time.fixedDeltaTime);
    }

    public void Walk(bool grounded) {
        if (grounded) {
            Grounded();
        } else {
            Airborne();
        }
    }
}
