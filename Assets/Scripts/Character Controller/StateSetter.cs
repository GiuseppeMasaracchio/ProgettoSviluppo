using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform entity;
    private bool approaching;

    private Vault vault;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();    
    }

    void Update() {
        //Debug.Log(rb.position);
        if (rb.velocity.y == 0f) { 
            vault.SetPlayerState("Idle"); 
        }
        if (rb.velocity.y < -1f) { 
            vault.SetPlayerState("Airborne");
            ApproachingGround();
        }
        if (rb.velocity.y > 1f) { vault.SetPlayerState("Jumping"); }
        if (rb.velocity.x != 0f) { vault.SetPlayerState("Walking"); }
        if (rb.velocity.z != 0f) { vault.SetPlayerState("Walking"); }
    }

    private void ApproachingGround() {
        approaching = Physics.Raycast(entity.transform.position, Vector3.down, 1.5f);
        if (approaching) { 
            vault.SetPlayerState("Approaching");
            return;
        }
    }
}
