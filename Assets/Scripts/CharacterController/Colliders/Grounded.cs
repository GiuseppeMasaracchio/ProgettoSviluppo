using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] TPCharacterController _ctx;
    
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = true;
        }
        else return;                
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = false;
        }
        else return;
    }
}
