using UnityEngine;

public class Stucked : MonoBehaviour {
    [SerializeField] CompanionController _ctx;

    private void OnTriggerEnter(Collider other) {
        if (_ctx.IsOperative && !_ctx.IsMoving) {
            _ctx.IsStuck = true;
            _ctx.IsOperative = false;
        }
    }

    private void OnTriggerStay(Collider other) {        
        if (_ctx.IsOperative && !_ctx.IsMoving) {
            _ctx.IsStuck = true;
            _ctx.IsOperative = false;
        }
    }
    private void OnTriggerExit(Collider other) {
        if ( _ctx.IsStuck && !_ctx.IsMoving) {            
            _ctx.IsStuck = false;
            _ctx.IsOperative = true;
        }
    }
}
