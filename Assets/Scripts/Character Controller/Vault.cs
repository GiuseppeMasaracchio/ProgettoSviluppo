using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour {
    public float movespeed = 2400f;
    public float airborne = 0.8f;
    public float sens = 100f;
    public float jumpheight = 12f;
    public float jumpcd = 2f;

    public string playerstate = "Idle";
    public bool grounded;

    void Start() {
        
    }

    void Update() {
        //Debug.Log(grounded);
        Debug.Log(playerstate);    
    }

    public void SetGrounded(bool grounded) {
        this.grounded = grounded;
    }

    public bool GetGrounded() {
        return grounded;
    }

    public void SetPlayerState(string state) {
        playerstate = state;
    }

    public string GetPlayerState() {
        return playerstate;
    }

    public float Get(string varname) {
        switch (varname) {
            case "movespeed":
                return movespeed;
            case "airborne":
                return airborne;
            case "sens":
                return sens;
            case "jumpheight":
                return jumpheight;
            case "jumpcd":
                return jumpcd;
            default:
                return 0f;
        }
    }

    public void Set(string varname, float value) {
        switch (varname) {
            case "movespeed":
                movespeed = value;
                break;
            case "airborne":
                airborne = value;
                break;
            case "sens":
                sens = value;
                break;
            case "jumpheight":
                jumpheight = value;
                break;
            case "jumpcd":
                jumpcd = value;
                break;
            default:
                Debug.Log("Vault internal error: invalid query");
                break;
        }
    }
}
