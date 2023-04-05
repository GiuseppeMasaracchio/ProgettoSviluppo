using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To be renamed MorePhysics
public class Grounded : MonoBehaviour
{
    [SerializeField] private GameObject entity;

    private CapsuleCollider entitycollider;
    private Rigidbody rb;

    private Vector3 startcapsule;
    private Vector3 endcapsule;

    private bool grounded;
    private float acceleration;

    private Vault vault;
    
    private float cap = -49.05f;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        rb = entity.GetComponent<Rigidbody>();
        entitycollider = entity.GetComponent<CapsuleCollider>();
    }

    public void SetGround() {
        startcapsule = entitycollider.center + entity.transform.position + Vector3.up * .1f;
        endcapsule = entitycollider.center + entity.transform.position + Vector3.down * .2f;
        grounded = Physics.CheckCapsule(startcapsule, endcapsule, .38f, LayerMask.GetMask("Ground"));
        if (grounded) { rb.velocity.Set(rb.velocity.x, 0f, rb.velocity.z); }
        vault.SetGrounded(grounded);
    } 

    private float SetAcceleration() {
        if (vault.GetGrounded()) {
            acceleration = 0f;
            return acceleration; 
        } else {
            acceleration += -9.81f * 15f * Time.deltaTime;
            acceleration = Mathf.Clamp(acceleration, cap, 0f);
            return acceleration;
        }
    }

    public void AdditionalGravity() {
        if (rb.velocity.y >= 0f) {
            return; 
        } else {
            rb.AddForce(Vector3.up * SetAcceleration(), ForceMode.Acceleration);
            return;
        }

    }
}
