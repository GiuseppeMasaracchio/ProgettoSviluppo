using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] TPCharacterController _ctx;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = true;
            //_ctx.IsAirborne = false;
        }
        //Debug.Log("Entering " + other.name);
    }   
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {            
            _ctx.IsGrounded = false;
            //_ctx.IsAirborne = true;
        }
        //Debug.Log("Exiting " + other.name);
    }
}
