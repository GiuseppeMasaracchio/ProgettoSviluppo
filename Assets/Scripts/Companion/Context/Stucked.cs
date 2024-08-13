using UnityEngine;

public class Stucked : MonoBehaviour {
    [SerializeField] CompanionController _ctx;
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsStuck = true;
        }
        if (other.tag == "Walls") {
            _ctx.IsStuck = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            _ctx.IsStuck = false;
        }
        if (other.tag == "Walls") {
            _ctx.IsStuck = false;
        }
    }
}
