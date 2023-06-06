using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePhysics : MonoBehaviour {
    [SerializeField] private GameObject entity;
    [SerializeField] private float gforce;
    [SerializeField] private float cap = -49.05f;

    private Rigidbody rb;

    //private Vault vault;
    
    private float acceleration;

    void Awake() {
        //vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        rb = entity.GetComponent<Rigidbody>();
    }

    void Update() {
        AdditionalGravity();
    }

    private float SetAcceleration() {
        if (Vault.GetGrounded()) {
            acceleration = 0f;
            return acceleration; 
        } else {
            acceleration += -9.81f * gforce * Time.fixedDeltaTime;
            acceleration = Mathf.Clamp(acceleration, cap, 0f);
            return acceleration;
        }
    }

    public void AdditionalGravity() {
        if (rb.velocity.y < 1f) {
            rb.AddForce(Vector3.up * SetAcceleration(), ForceMode.Acceleration);
            return; 
        } else {
            return;
        }

    }
}
