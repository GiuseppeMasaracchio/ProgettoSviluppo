using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {

    //Reference to script
    private CameraRotation cam;
    private Movement move;
    private PlayerInput playerInput;
    private OptionsMenuScript menuScript;

    void Awake() {
        //cam = GameObject.Find("ScriptsHolder").GetComponent<CameraRotation>();
        //move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
        menuScript = GetComponent<OptionsMenuScript>();
    }


    public void OnMove(InputValue input) {
        move.SetMoveInput(input.Get<Vector2>());

        //Debug.Log(input.Get<float>());
    }

    public void OnFire(InputValue input) {
        if (input.Get() == null) { return; }

        //Debug.Log(input.Get<float>());
    }

    public void OnLook(InputValue input) {
        cam.SetMouseInput(input.Get<Vector2>());

        //Debug.Log(input.Get<float>());
    }

    public void OnJump(InputValue input) {
<<<<<<< HEAD:Assets/Scripts/InputHandler.cs
        if (input.Get() == null) return;
        move.JumpInput(input.Get<float>());
=======
        move.SetJumpInput(input.Get<float>());

        //Debug.Log(input.Get<float>());
>>>>>>> bf0f5fdc6559207ff5df62240a5c0ad139475b02:Assets/Scripts/Character Controller/InputHandler.cs
    }

    public void OnPause(InputValue input) {
        if (input.Get() == null) return;
        menuScript.Pause();
    }
}
