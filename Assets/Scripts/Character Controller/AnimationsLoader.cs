using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsLoader : MonoBehaviour {
    private Animator animator;
    private Vault vault;

    private Vector2 current = Vector2.zero;
    private Vector2 target = Vector2.zero;
    
    private Vector2 lerpvalue = Vector2.zero;
    private Vector2 iterator = Vector2.zero;

    private Vector2 idle = Vector2.zero;
    private Vector2 walking = new Vector2(0f, 1f);
    private Vector2 jumping = new Vector2(-.33f, -.33f);
    private Vector2 airborne = new Vector2(-.66f, -.66f);
    private Vector2 approaching = new Vector2(-1f, -1f);
    private Vector2 attacking = new Vector2(-1f, 1f);

    [SerializeField] private float animationspeed = 1f;

    private string state;


    void Awake() {
        animator = GameObject.Find("PlayerAsset").GetComponent<Animator>();
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }


    void Update() {
        state = vault.GetPlayerState();

        switch(state) {
            case "Idle":
                Load(idle);
                break;
            case "Walking":
                Load(walking);
                break;
            case "Jumping":
                Load(jumping);
                break;
            case "Airborne":
                Load(airborne);
                break;
            case "Approaching":
                Load(approaching);
                break;
            case "Attacking":
                Load(attacking);
                break;
        }
    }

    private void Load(Vector2 animation) {
        current.x = animator.GetFloat("xAxis");
        current.y = animator.GetFloat("yAxis");

        target = animation;

        if (current.x > target.x) {
            iterator.x -= .11f * animationspeed * Time.deltaTime;
        } else if (current.x < target.x) {
            iterator.x += .11f * animationspeed * Time.deltaTime;
        }

        if (current.y > target.y) {
            iterator.y -= .11f * animationspeed * Time.deltaTime;
        } else if (current.y < target.y) {
            iterator.y += .11f * animationspeed * Time.deltaTime;
        }

        lerpvalue = Vector2.Lerp(current, target, animationspeed);

        animator.SetFloat("xAxis", lerpvalue.x);
        animator.SetFloat("yAxis", lerpvalue.y);
    }
}
