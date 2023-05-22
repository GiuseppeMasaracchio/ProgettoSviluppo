using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetter : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform entity;

    private Vector2 horizontalvel;

    private bool isApproaching;
    private bool isJumping;

    //private Vault vault;

    void Awake() {
        //vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }

    void Update() {

        if (Vault.GetGrounded()) {
            DetectWalking();
        }

        if (rb.velocity.y < -1f) {
            isJumping = false;
            ApproachingGround();
        }
        if (isJumping) { Vault.SetPlayerState("Jumping"); }
    }

    private void ApproachingGround() {
        if (isApproaching) {
            Vault.SetPlayerState("Approaching");
            return;
        } else {
            Vault.SetPlayerState("Airborne");
            return;
        }
    }

    private void DetectWalking() {
        horizontalvel = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalvel != Vector2.zero) {
            Vault.SetPlayerState("Walking");
        } else {
            Vault.SetPlayerState("Idle");
        }

    }

    public void DetectJump(bool input) {
        if (input) {
            isJumping = true;
        } else {
            isJumping = false;
        }
    }

    public void SetApproaching(bool info) {
        isApproaching = info;
    }
}
