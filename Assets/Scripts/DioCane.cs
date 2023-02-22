using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DioCane : MonoBehaviour
{
    [SerializeField] CameraRotation cam;
    [SerializeField] Rigidbody player;
    [SerializeField] Movement move;
    void Start() {
        
    }

    public void OnMove(InputValue input) {
        move.Dir(input.Get<Vector2>());
    }

    public void OnFire(InputValue input) {
        Debug.Log(input.Get());
        Debug.Log("puzzo");
    }

    public void OnLook(InputValue input) {
        Debug.Log(input.Get<Vector2>());
        cam.xRot(input.Get<Vector2>());
    }
}
