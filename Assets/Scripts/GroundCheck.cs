using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundCheck : MonoBehaviour
{
    private bool grounded;

    private Transform pgTransform;

  

    // Update is called once per frame
    void Update() {
        pgTransform = GameObject.Find("Player").transform;
        grounded = Physics.Raycast(pgTransform.position, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));    
    }

    public bool GetGroundCheck() {
        return grounded;
    }
}
