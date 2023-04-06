using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsLoader : MonoBehaviour {
    private Animator animator;
    private Vault vault;

    //private float xAxis = 0f;
    //private float yAxis = 0f;
    private Vector2 coordinates = Vector2.zero;

    private string state;

    void Awake() {
        animator = GameObject.Find("PlayerAsset").GetComponent<Animator>();
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }


    void Update()
    {
        state = vault.GetPlayerState();

        switch(state) {
            case "Idle":
                animator.SetFloat("xAxis", 0f);
                animator.SetFloat("yAxis", 0f);
                break;
            case "Walking":
                animator.SetFloat("xAxis", 0f);
                animator.SetFloat("yAxis", 1f);
                break;
            case "Jumping":
                animator.SetFloat("xAxis", -.33f);
                animator.SetFloat("yAxis", -.33f);
                break;
            case "Airborne":
                animator.SetFloat("xAxis", -.66f);
                animator.SetFloat("yAxis", -.66f);
                break;
            case "Approaching":
                animator.SetFloat("xAxis", -1f);
                animator.SetFloat("yAxis", -1f);
                break;
            case "Attacking":
                animator.SetFloat("xAxis", 0f);
                animator.SetFloat("yAxis", 0f);
                break;
        }
    }
}
