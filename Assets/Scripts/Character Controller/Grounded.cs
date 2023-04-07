using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private Vault vault;
    private Rigidbody rb;

    void Awake() {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }



    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            rb.velocity.Set(rb.velocity.x, 0.1f, rb.velocity.z);
            vault.SetGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            vault.SetGrounded(false);
        }
    }
}
