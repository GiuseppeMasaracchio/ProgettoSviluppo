using UnityEngine;

public class Stucked : MonoBehaviour {
    [SerializeField] CompanionController _ctx;

    private void OnTriggerEnter(Collider other) {
        _ctx.IsStuck = true;
        _ctx.IsOperative = false;
    }

    private void OnTriggerStay(Collider other) {
        /*
        if (other.tag == "Ground") {
            _ctx.IsStuck = true;
            _ctx.IsOperative = false;
        } else {
            _ctx.IsStuck = false;
            _ctx.IsOperative = true;
        }

        if (other.tag == "Walls") {
            _ctx.IsStuck = true;
            _ctx.IsOperative = false;
        }
        else {
            _ctx.IsStuck = false;
            _ctx.IsOperative = true;
        }
        */
        _ctx.IsStuck = true;
        _ctx.IsOperative = false;
    }
    private void OnTriggerExit(Collider other) {
        /*
        if (other.tag == "Ground") {
            _ctx.IsStuck = false;
            _ctx.IsOperative = true;
        }
        if (other.tag == "Walls") {
            _ctx.IsStuck = false;
            _ctx.IsOperative = true;
        }
        */
        _ctx.IsStuck = false;
        _ctx.IsOperative = true;
    }
}
