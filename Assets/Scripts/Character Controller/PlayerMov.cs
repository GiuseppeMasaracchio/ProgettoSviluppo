using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour {

    private GameObject scriptsholder;
    private Movement move;
    private Grounded gcheck;
    private OptionsMenuScript optionsScript;


    void Awake() {
        scriptsholder = GameObject.Find("ScriptsHolder");
        move = scriptsholder.GetComponent<Movement>();
        gcheck = scriptsholder.GetComponent<Grounded>();
        optionsScript = scriptsholder.GetComponent<OptionsMenuScript>();
        
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Start() {
        
    }

    void FixedUpdate() {
        
    }


    void Update() {
        gcheck.SetGround();
        gcheck.AdditionalGravity();

        move.JumpCheck();
        move.Move();
        
    }
}