using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetter : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform entity;

    private Vector2 horizontalvel;

    //private Vector3 entitycenter;
    //private Vector3 entityhalfdim;

    private bool isApproaching;
    private bool isJumping;

    private Vault vault;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }

    void Update() {
        //entityhalfdim = new Vector3(entity.transform.localScale.x / 4.2f, entity.transform.localScale.y / 2f, entity.transform.localScale.z / 4.2f);
        //entitycenter = entity.transform.position - Vector3.up * .1f;
        /*
        if (rb.velocity.y == 0f) {
            DetectWalking();
        } */

        if (vault.GetGrounded()) {
            DetectWalking();
        }

        if (rb.velocity.y < -1f) {
            ApproachingGround();
        }
        if (isJumping) { vault.SetPlayerState("Jumping"); }
    }

    private void ApproachingGround() {
        //isApproaching = Physics.Raycast(entity.transform.position, Vector3.down, 1.5f, LayerMask.GetMask("Ground"));
        //isApproaching = Physics.BoxCast(entitycenter, entityhalfdim, Vector3.down, entity.transform.rotation, 1.5f, LayerMask.GetMask("Ground"));
        //isApproaching
        if (isApproaching) {
            vault.SetPlayerState("Approaching");
            return;
        } else {
            vault.SetPlayerState("Airborne");
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
