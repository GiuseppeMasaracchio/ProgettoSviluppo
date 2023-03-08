using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    public float movespeed;
    public float airborne;
    public float sens;
    public string playerstate;
    public bool grounded;

    void Start() {
        movespeed = 400f;
        airborne = .8f;
        sens = 80f;
        playerstate = "Default";

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
            default:
                Debug.Log("Vault internal error: invalid query");
                break;
        }
    }
}
