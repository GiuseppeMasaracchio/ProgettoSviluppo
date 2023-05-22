using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rigidbody_extension
{
    public static void Jump(this Rigidbody rb) {
        //rb.ResetInertiaTensor();
        rb.velocity.Set(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * Vault.Get("movespeed"), ForceMode.Acceleration);
    }

    public static void Run(this Rigidbody rb, Movement m) {
        rb.AddForce(m.Direction() * Vault.Get("movespeed") * (Enums.GetAirMultiplier() / 10f) * Time.deltaTime, ForceMode.Force);
    }
}
