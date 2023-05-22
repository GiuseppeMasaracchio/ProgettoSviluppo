using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector2 moveinput = Vector2.zero;
    private Vector3 direction;

    private Rigidbody player;
    private Transform orientation;
    private Transform asset;
    private Transform assetforward;

    private bool buttonstate;
    public bool cdstate = true;

    private StateSetter state;

    void Awake() {
        state = GameObject.Find("ScriptsHolder").GetComponent<StateSetter>();

        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        orientation = GameObject.Find("Player").transform;
        asset = GameObject.Find("PlayerAsset").transform;
        assetforward = GameObject.Find("PlayerForward").transform;

    }

    void Update() {
        if (moveinput != Vector2.zero) {
            orientation.forward = assetforward.forward;
            return; 
        }
    }

    //Movement related methods
    public void SetMoveInput(Vector2 input) {
        moveinput = input;
    }

    public Vector3 Direction() {
        direction = orientation.forward * moveinput.y + orientation.right * moveinput.x;
        return direction;
    }


    public void Walk() {
        if (Direction() == Vector3.zero) { return; }
        asset.forward = Direction();
        player.Run(this);
        SpeedControl();
    }

    public void SpeedControl() {
        Vector3 flatvelocity = new Vector3(player.velocity.x, 0f, player.velocity.z); 
        if (flatvelocity.magnitude > Vault.Get("movespeed")) {
            Vector3 limvelocity = flatvelocity.normalized * Vault.Get("movespeed");
            player.velocity = new Vector3(limvelocity.x, player.velocity.y, limvelocity.z);
        }
    }

    //Jump related methods
    public void SetJumpInput(float input) {
        //Metodo che setta lo stato dell'input
        if (input.Equals(1f)) {
            buttonstate = true;
            cdstate = false;
        } else if (input.Equals(0f)) {
            buttonstate = false;
            cdstate = true;
        } 
    }

    public void JumpCheck() {
        //Metodo che viene eseguito nell'update del player
        if (!Vault.GetGrounded()) {
            return;
        }
        else {
            InputCheck();
        }
    }

    public void InputCheck() {
        //Metodo che controlla l'input 
        if (!buttonstate) {
            return; 
        } else {
            Jump();
        }
    }

    public void Jump() {
        if (cdstate) {
            return; 
        } else {
            cdstate = true;

            player.Jump();
            state.DetectJump(true);
            Invoke(nameof(ResetCd), Vault.Get("jumpcd"));
        }
    }

    public void ResetCd() {
        cdstate = false;
    }
}
