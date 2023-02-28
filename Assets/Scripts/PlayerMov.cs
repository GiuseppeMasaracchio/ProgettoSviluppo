using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour {

    private Movement move;
    private GroundCheck gCheck;

    void Awake() {
        move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
        gCheck = GameObject.Find("ScriptsHolder").GetComponent<GroundCheck>();
    }

    void Start() {
        
    }

    void Update() {
        
        move.Walk(gCheck.GetGroundCheck());
    }
}
