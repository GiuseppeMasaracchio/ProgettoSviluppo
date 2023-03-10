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
        //move.Direction(input.Get<Vector2>());
        
        //Debug.Log(input.Get());
    }

    public void OnFire(InputValue input) {
        if (input.Get() == null) { return; }
        Debug.Log(input.Get());
    }

    public void OnLook(InputValue input) {
        //Debug.Log(input.Get<Vector2>().normalized);
        cam.ScreenPosition(input.Get<Vector2>().normalized);
    
    }

    public void OnJump(InputValue input) {
        if (input.Get() == null) return;
        move.JumpInput(input.Get<float>());
    }

    
    
}
