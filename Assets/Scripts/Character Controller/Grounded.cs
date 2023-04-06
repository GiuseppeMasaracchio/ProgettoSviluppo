using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private Vault vault;

    void Awake()
    {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        vault.SetGrounded(true);
    }

    private void OnTriggerExit(Collider other) {
        vault.SetGrounded(false);
    }
}
