using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputHandler : MonoBehaviour {
    //Reference to object
    private Camera screenspace;

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
        Vector2 position = new Vector2(input.Get<Vector2>().x - (screenspace.pixelWidth / 2), input.Get<Vector2>().y - (screenspace.pixelHeight / 2));
        cam.ScreenPosition(position);

    }

    public void OnJump(InputValue input) {
        move.Jump();
        
        
    }
}
