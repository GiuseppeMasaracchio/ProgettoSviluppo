using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    private float movespeed;
    private float airborne;
    private float sens;
    private float jumpHeight;

    void Awake() {
        movespeed = 400f;
        airborne = .8f;
        sens = 20f;
        jumpHeight = 10f;
    }
    public float Get(string var) {
        switch (var) {
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

    public void Set(string var, float value) {
        switch (var) {
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
