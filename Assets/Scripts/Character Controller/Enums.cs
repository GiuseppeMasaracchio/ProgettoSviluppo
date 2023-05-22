using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums {
    public enum Airmultiplier { grounded = 10, airborne = 8 };

    static Airmultiplier value;

    public static void SetAirMultiplier() {
        if (Vault.GetGrounded()) {
            value = Airmultiplier.grounded;     
        } else {
            value = Airmultiplier.airborne;
        }
    }

    public static int GetAirMultiplier() {
        return (int)value;
    }
}


