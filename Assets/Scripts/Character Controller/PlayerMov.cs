using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour {

    private GameObject scriptsholder;
    private Movement move;
    private MorePhysics physics;

    void Awake() {
        scriptsholder = GameObject.Find("ScriptsHolder");
        move = scriptsholder.GetComponent<Movement>();
        physics = scriptsholder.GetComponent<MorePhysics>();
        
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update() {
        //physics.AdditionalGravity();

        move.JumpCheck();
        move.Move();
        
    }
}
