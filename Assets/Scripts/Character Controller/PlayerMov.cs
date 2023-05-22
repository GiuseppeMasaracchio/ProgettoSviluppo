using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMov : MonoBehaviour {

    private GameObject scriptsholder;
    private Movement move;


    void Awake() {
        scriptsholder = GameObject.Find("ScriptsHolder");
        move = scriptsholder.GetComponent<Movement>();

    }

    private void Start() {
        
    }

    void Update() {
        Enums.SetAirMultiplier();
        move.JumpCheck();
        move.Walk();

    }
}
