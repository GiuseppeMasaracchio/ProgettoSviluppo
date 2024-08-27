using UnityEngine;

public class Stucked : MonoBehaviour {
    [SerializeField] CompanionController _ctx;

    private void OnTriggerEnter(Collider other) {        
        if (other.tag != "PlayerAttacks" && _ctx.IsOperative && !_ctx.IsMoving) {
            _ctx.IsStuck = true;
            _ctx.IsOperative = false;
        }
    }

    private void OnTriggerStay(Collider other) {

        if (other.tag != "PlayerAttacks" && _ctx.IsOperative && !_ctx.IsMoving) {
            _ctx.IsStuck = true;
            _ctx.IsOperative = false;
        }

        if (_ctx.IsStuck) {

            _ctx.IsUnstucking = true;
        }

    }
    private void OnTriggerExit(Collider other) {
        if (other.tag != "PlayerAttacks" && _ctx.IsStuck && !_ctx.IsMoving) {
            _ctx.IsUnstucking = false;
            _ctx.IsStuck = false;
            _ctx.IsOperative = true;
        }
    }
}
