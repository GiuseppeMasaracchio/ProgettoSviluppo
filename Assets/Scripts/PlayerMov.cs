using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour {
    [SerializeField] Transform camHolder;
    [SerializeField] float horizontalSens;
    [SerializeField] Rigidbody player;

    public CameraRotation cam;
    public Movement move;

    void Awake() {
        cam = camHolder.GetComponent<CameraRotation>();
        move = player.GetComponent<Movement>();

    }

    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    void Update() {
        camHolder.rotation = cam.CamRot(horizontalSens);
        player.AddForce(move.Move().normalized * 500f * Time.fixedDeltaTime);
        
    }
}
