using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour {

    private GameObject scriptsholder;
    private Movement move;
    private Grounded gcheck;

    void Awake() {
        scriptsholder = GameObject.Find("ScriptsHolder");
        move = scriptsholder.GetComponent<Movement>();
        gcheck = scriptsholder.GetComponent<Grounded>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start() {
        
    }

    void Update() {
        gcheck.isGrounded();
        move.GroundJumpCheck();
        move.Walk();  
        
    }

    void FixedUpdate() {
        
    }
}
