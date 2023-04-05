using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform entity;

    private Vector2 horizontalvel;

    private bool approaching;
    private bool jump;

    private Vault vault;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();    
    }

    void Update() {
        if (rb.velocity.y == 0f) {
            DetectWalking();
        }
        if (rb.velocity.y < -1f) { 
            vault.SetPlayerState("Airborne");
            ApproachingGround();
        }
        if (jump) { vault.SetPlayerState("Jumping"); }
    }

    private void ApproachingGround() {
        approaching = Physics.Raycast(entity.transform.position, Vector3.down, 1.5f);
        if (approaching) { 
            vault.SetPlayerState("Approaching");
            return;
        }
    }

    private void DetectWalking() {
        horizontalvel = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalvel != Vector2.zero) { 
            vault.SetPlayerState("Walking"); 
        } else {
            vault.SetPlayerState("Idle");
        }
        
    }

    public void DetectJump(float input) {
        if (input == 1f) { 
            jump = true; 
        } else {
            jump = false;
        }
    }
}
