using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundCheck : MonoBehaviour
{
    private bool grounded;

    private Transform pgTransform;

    private void Start() {
        pgTransform = GameObject.Find("Player").transform;
    }


    // Update is called once per frame
    void Update() {
        grounded = Physics.Raycast(pgTransform.position, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));    
    }

    public bool GetGroundCheck() {
        return grounded;
    }
}
