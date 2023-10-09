using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approaching : MonoBehaviour
{
    [SerializeField] TPCharacterController _ctx;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsApproaching = true;
            //Debug.Log("Setting approaching true");
        }
    }
}
