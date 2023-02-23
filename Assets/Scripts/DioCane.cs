using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DioCane : MonoBehaviour
{
    [SerializeField] CameraRotation cam;
    [SerializeField] Rigidbody player;
    [SerializeField] Movement move;
    [SerializeField] Camera sCam;
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
        Vector2 position = new Vector2(input.Get<Vector2>().x - (sCam.pixelWidth /2), input.Get<Vector2>().y - (sCam.pixelHeight / 2));
        //sCam.ScreenToWorldPoint(position);
        
        Debug.Log(position);
        cam.xRot(position);
    }
}
