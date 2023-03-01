using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputHandler : MonoBehaviour {

    //Reference to script
    private CameraRotation cam;
    private Movement move;

    void Awake() {
        screenspace = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam = GameObject.Find("ScriptsHolder").GetComponent<CameraRotation>();
        move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
    }

    void Start() {
       
    }

    public void OnMove(InputValue input) {
        move.Direction(input.Get<Vector2>());

    }

    public void OnFire(InputValue input) {
        Debug.Log(input.Get());

    }

    public void OnLook(InputValue input) {
        Vector2 position = input.Get<Vector2>();
        cam.ScreenPosition(position);

    }

    public void OnJump(InputValue input) {
        Debug.Log("Zaltado");
        move.Jump();
    }
}
