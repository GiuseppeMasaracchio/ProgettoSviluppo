using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Anim {
    dead,
    idle,
    walk,
    attack1,
    jump,
    fall,
    dash,
    damage
}

public class AnimHandler : MonoBehaviour{
    Animator _animator;

    Dictionary<Anim, Vector2> animList = new Dictionary<Anim, Vector2>(10);
    public AnimHandler() {
        //animList[Anim.dead] = new Vector2(x, y);                      //0
        animList[Anim.idle] = new Vector2(0f, 0f);                       //1
        animList[Anim.walk] = new Vector2(0f, 1f);                       //2
        animList[Anim.attack1] = new Vector2(-1f, 1f);                    //3
        animList[Anim.jump] = new Vector2(-.33f, -.33f);                 //4
        animList[Anim.fall] = new Vector2(-.66f, -.66f);                 //5
        //animList[Anim.dash] = new DashState(_context, this);             //6
        //animList[Anim.damage] = new DamageState(_context, this);         //7
    }

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        
    }

    public void Play(Vector2 clip) {
        //StartCoroutine(LoadClip(clip));
        _animator.SetFloat("xAxis", clip.x);
        _animator.SetFloat("yAxis", clip.y);
        
    }    
    
    IEnumerator LoadClip(Vector2 target) {
        //Debug.Log("Starting Coroutine");
        float currentx = _animator.GetFloat("xAxis");
        float currenty = _animator.GetFloat("yAxis");
        Vector2 current = new Vector2(currentx, currenty);

        while (Vector2.Distance(current, target) != 0) {
            Vector2 lerpvalue = Vector2.Lerp(current, target, .12f);

            _animator.SetFloat("xAxis", lerpvalue.x);
            _animator.SetFloat("yAxis", lerpvalue.y);

            current = lerpvalue;
            yield return null;
        }

        //StopCoroutine(LoadClip(target));
    }

    public Vector2 Dead() {
        return animList[Anim.dead];
    }
    public Vector2 Damage() {
        return animList[Anim.damage];
    }
    public Vector2 Idle() {
        return animList[Anim.idle];
    }
    public Vector2 Walk() {
        return animList[Anim.walk];
    }
    public Vector2 Attack1() {
        return animList[Anim.attack1];
    }
    public Vector2 Jump() {
        return animList[Anim.jump];
    }
    public Vector2 Dash() {
        return animList[Anim.dash];
    }
    public Vector2 Fall() {
        return animList[Anim.fall];
    }
}
