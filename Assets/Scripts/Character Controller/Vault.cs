using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Playables;

public static class Vault {
    private static float movespeed = 2400f;
    private static float airborne = 0.8f;
    private static float sens = 100f;
    private static float jumpheight = 12f;
    private static float jumpcd = 2f;

    private static string playerstate = "Idle";
    private static bool grounded;

    private static Vector3 checkpointPosition;


   
  
    public static Vector3 GetCheckpoint() {
        return checkpointPosition;
    }
    public static void SetCheckpoint(Vector3 input) {
        checkpointPosition = input;
    }
    public static void SetGrounded(bool input) {
        grounded = input;
    }

    public static bool GetGrounded() {
        return grounded;
    }

    public static void SetPlayerState(string state) {
        playerstate = state;
    }

    public static string GetPlayerState() {
        return playerstate;
    }

    public static float Get(string varname) {
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

    public static void Set(string varname, float value) {
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
