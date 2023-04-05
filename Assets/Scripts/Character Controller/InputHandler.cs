using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {

    //Reference to script
    private CameraRotation cam;
    private Movement move;
    private StateSetter state;

    void Awake() {
        cam = GameObject.Find("ScriptsHolder").GetComponent<CameraRotation>();
        move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
        state = GameObject.Find("ScriptsHolder").GetComponent<StateSetter>();

    }

    public void OnMove(InputValue input) {
        move.SetMoveInput(input.Get<Vector2>());
        //Debug.Log(input.Get());
    }

    public void OnFire(InputValue input) {
        if (input.Get() == null) { return; }
        //Debug.Log(input.Get<float>());
    }

    public void OnLook(InputValue input) {
        cam.SetMouseInput(input.Get<Vector2>());

        //Debug.Log(input.Get<Vector2>());
    }

    public void OnJump(InputValue input) {
        move.SetJumpInput(input.Get<float>());

        state.DetectJump(input.Get<float>());

        //Debug.Log(input.Get<float>());
    }

}
