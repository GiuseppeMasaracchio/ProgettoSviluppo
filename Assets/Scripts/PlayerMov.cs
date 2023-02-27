using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour {

    private Movement move;

    void Awake() {
        move = GameObject.Find("ScriptsHolder").GetComponent<Movement>();
    }

    void Start() {
        
    }

    void Update() {
        move.Grounded();
        
    }
}
