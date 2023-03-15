using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {

    //Reference to script
    private CameraRotation cam;
    private Movement move;
    private OptionsMenuScript options;
    private GameObject scriptsHolder;

    void Awake() {
        scriptsHolder = GameObject.Find("ScriptsHolder");

        cam = scriptsHolder.GetComponent<CameraRotation>();
        move = scriptsHolder.GetComponent<Movement>();
        options = scriptsHolder.GetComponent<OptionsMenuScript>();
       
    }


    public void OnMove(InputValue input) {
        move.SetMoveInput(input.Get<Vector2>());

    }

    public void OnFire(InputValue input) {
        if (input.Get() == null) { return; }
    }

    public void OnLook(InputValue input) {
        cam.SetMouseInput(input.Get<Vector2>());

    }

    public void OnJump(InputValue input) {

        move.SetJumpInput(input.Get<float>());

    }

    public void OnPause(InputValue input) {
        if (input.Get() == null) return;
        
        options.OptionsModal();
    }
}
