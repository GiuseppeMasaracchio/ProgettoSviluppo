using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vault {
    private static float movespeed = 2000f;
    private static float sens = 100f;
    private static float jumpcd = 3f;

    private static string playerstate = "Idle";
    private static bool isGrounded;

    public static void SetGrounded(bool grounded) {
        isGrounded = grounded;
    }

    public static bool GetGrounded() {
        return isGrounded;
    }

    public static void SetPlayerState(string state) {
        if (state == playerstate) { return; }
        playerstate = state;
    }

    public static string GetPlayerState() {
        return playerstate;
    }

    public static float Get(string varname) {
        switch (varname) {
            case "movespeed":
                return movespeed;
            case "sens":
                return sens;
            case "jumpcd":
                return jumpcd;
            default:
                return 0f;
        }
    }

    public static void Set(string varname, float value) {
        switch (varname) {
            case "movespeed":
                movespeed = value;
                break;
            case "sens":
                sens = value;
                break;
            case "jumpcd":
                jumpcd = value;
                break;
            default:
                Debug.LogError("Vault internal error: invalid query");
                break;
        }
    }
}
