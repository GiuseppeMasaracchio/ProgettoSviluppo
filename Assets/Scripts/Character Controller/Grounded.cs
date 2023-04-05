using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To be renamed MorePhysics
public class Grounded : MonoBehaviour
{
    [SerializeField] private GameObject entity;
    private CapsuleCollider entitycollider;

    private Rigidbody rb;

    private bool grounded;
    private bool check;

    private Vector3 startcapsule;
    private Vector3 endcapsule;

    private float acceleration;
    private float cap;

    private RaycastHit hitinfo;

    private Vault vault;

    void Awake() {
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        rb = entity.GetComponent<Rigidbody>();
        entitycollider = entity.GetComponent<CapsuleCollider>();
    }

    void Start() {
        cap = -9.81f * 5f;
            
    }

    public void SetGround() {
        //grounded = Physics.Raycast(entity.transform.position, Vector3.down, entity.transform.localScale.y / 2 + 0.3f, LayerMask.GetMask("Ground"));
        /*
        check = Physics.SphereCast(entity.transform.position - (Vector3.up * .5f), .6f, Vector3.down, out hitinfo, Mathf.Infinity);
        if (check) { 
            grounded = false; 
        } else {
            grounded = true; 
        } */
        startcapsule = entitycollider.center + entity.transform.position + Vector3.up * .1f;
        endcapsule = entitycollider.center + entity.transform.position + Vector3.down * .2f;
        grounded = Physics.CheckCapsule(startcapsule, endcapsule, .38f, LayerMask.GetMask("Ground"));

        vault.SetGrounded(grounded);
        Debug.Log(grounded);
    } 

    private float SetAcceleration() {
        if (vault.GetGrounded()) {
            acceleration = 0f;
            return acceleration; 
        } else {
            acceleration += -9.81f * 8f * Time.deltaTime;
            return Mathf.Clamp(acceleration, cap, 0f);
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
