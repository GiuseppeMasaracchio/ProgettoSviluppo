using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour {

    private Movement move;
    private GroundCheck gCheck;

    void Awake() {
        move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
<<<<<<< HEAD
        gCheck = GameObject.Find("ScriptsHolder").GetComponent<GroundCheck>();
=======
        
        Cursor.lockState = CursorLockMode.Locked;
>>>>>>> bd7e1ba5bb6ba5198a5e8a19e9acda2db2b8b916
    }

    void Start() {
        
    }

    void Update() {
<<<<<<< HEAD
=======
        //move.Walk();  //Come parametro inserisco grounded (dal vault)
        move.JumpCheck();
>>>>>>> bd7e1ba5bb6ba5198a5e8a19e9acda2db2b8b916
        
        move.Walk(gCheck.GetGroundCheck());
    }
}
