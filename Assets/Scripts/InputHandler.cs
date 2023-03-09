using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {

    //Reference to script
    private CameraRotation cam;
    private Movement move;

    void Awake() {
        cam = GameObject.Find("ScriptsHolder").GetComponent<CameraRotation>();
        move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
      
    }

    public void OnMove(InputValue input) {
        move.Direction(input.Get<Vector2>());
        //Debug.Log(input.Get<Vector2>());

    }

    public void OnFire(InputValue input) {
        if (input.Get() == null) { return; }
        //Debug.Log(input.Get());
    }

    public void OnLook(InputValue input) {
        //if (input.Get<Vector2>() == Vector2.zero) { return; }
        Debug.Log(input.Get<Vector2>());
        cam.SetMouseInput(input.Get<Vector2>());

    }

    public void OnJump(InputValue input) {
        //if (input.Get<float>() == 1f) Debug.Log(input.Get<float>());
        //Debug.Log(input.Get<float>());
        move.JumpInput(input.Get<float>());
    }

}
