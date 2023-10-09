using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] TPCharacterController _ctx;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = true;
            //_ctx.IsFalling = false;
            //_ctx.IsIdle = true;
            //Debug.Log(_ctx.IsFalling);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = false;
            //Debug.Log("Setting grounded false");
        }
    }
}
