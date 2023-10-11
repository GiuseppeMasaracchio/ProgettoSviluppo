using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] TPCharacterController _ctx;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = true;
        }
    }   
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsGrounded = false;
        }
    }
}
