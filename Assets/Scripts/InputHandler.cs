using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {
    //Reference to object
    [SerializeField] Camera sCam;

    //Reference to script
    [SerializeField] CameraRotation cam;
    [SerializeField] Movement move;

    void Start() {

    }

    public void OnMove(InputValue input) {
        move.Direction(input.Get<Vector2>());

    }

    public void OnFire(InputValue input) {
        Debug.Log(input.Get());

    }

    public void OnLook(InputValue input) {
        Vector2 position = new Vector2(input.Get<Vector2>().x - (sCam.pixelWidth / 2), input.Get<Vector2>().y - (sCam.pixelHeight / 2));
        //Debug.Log(position);
        cam.ScreenPosition(position);

    }

    public void OnJump(InputValue input) {
        Debug.Log("Zaltato");
    }
}
