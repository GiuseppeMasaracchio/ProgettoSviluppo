using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
<<<<<<< HEAD
    private float movespeed;
    private float airborne;
    private float sens;
    private float jumpHeight;
=======
    public float movespeed;
    public float airborne;
    public float sens;
    public string playerstate;
>>>>>>> bd7e1ba5bb6ba5198a5e8a19e9acda2db2b8b916

    void Awake() {
        movespeed = 400f;
        airborne = .8f;
<<<<<<< HEAD
        sens = 20f;
        jumpHeight = 10f;
=======
        sens = 80f;
        playerstate = "Idle";

>>>>>>> bd7e1ba5bb6ba5198a5e8a19e9acda2db2b8b916
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
            case "jumpHeight":
                return jumpHeight;
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
        }
    }
}
