using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePhysics : MonoBehaviour {
    [SerializeField] private GameObject entity;

    private Rigidbody rb;

    //private Vault vault;
    
    private float acceleration;
    private float cap = -49.05f;

    void Awake() {
        //vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        rb = entity.GetComponent<Rigidbody>();
    }

    private float SetAcceleration() {
        if (Vault.GetGrounded()) {
            acceleration = 0f;
            return acceleration; 
        } else {
            acceleration += -9.81f * 15f * Time.deltaTime;
            acceleration = Mathf.Clamp(acceleration, cap, 0f);
            return acceleration;
        }
    }

    public void AdditionalGravity() {
        if (rb.velocity.y > 0f) {
            return; 
        } else {
            rb.AddForce(Vector3.up * SetAcceleration(), ForceMode.Acceleration);
            return;
        }

    }
}
