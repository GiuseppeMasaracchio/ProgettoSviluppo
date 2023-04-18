using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetter : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform entity;

    private Vector2 horizontalvel;

    private bool isApproaching;
    private bool isJumping;
    private bool isAttacking;

    //private Vault vault;

    void Awake() {
        //vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }

    void Update() {
        //if (Vault.GetGrounded()) {
        if (GenericsVault<bool>.GetPlayerInfo(true)) { 
            DetectWalking();
        }

        if (rb.velocity.y < -1f) {
            ApproachingGround();
        }
        //if (isJumping) { Vault.SetPlayerState("Jumping"); }
        if (isJumping) { GenericsVault<string>.SetPlayerInfo("Jumping"); }

        //if (isAttacking) Vault.SetPlayerState("Attacking");
        if (isJumping) { GenericsVault<string>.SetPlayerInfo("Attacking"); }

    }

    private void ApproachingGround() {
        if (isApproaching) {
            //Vault.SetPlayerState("Approaching");
            GenericsVault<string>.SetPlayerInfo("Approaching");
            return;
        } else {
            //Vault.SetPlayerState("Airborne");
            GenericsVault<string>.SetPlayerInfo("Airborne");
            return;
        }
    }

    private void DetectWalking() {
        horizontalvel = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalvel != Vector2.zero) {
            //Vault.SetPlayerState("Walking");
            GenericsVault<string>.SetPlayerInfo("Walking");
        } else {
            //Vault.SetPlayerState("Idle");
            GenericsVault<string>.SetPlayerInfo("Idle");
        }

    }

    public void DetectJump(bool input) {
        if (input) {
            isJumping = true;
        } else {
            isJumping = false;
        }
    }

    public void DetectAttack(bool input) {
        if (input == isAttacking) return;
        isAttacking = input;
    }
    public void SetApproaching(bool info) {
        isApproaching = info;
    }
}
