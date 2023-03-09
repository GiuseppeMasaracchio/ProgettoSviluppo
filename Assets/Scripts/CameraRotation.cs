using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    private Vector2 mousedelta = Vector2.zero;
    private float xaxis;
    private float yaxis;


    private Transform camHolder;
    private Transform player;
    private Vault vault;

    void Awake() {
        camHolder = GameObject.Find("CameraHolder").transform;
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        player = GameObject.Find("Player").transform;
    }

    public float yCamRot() {
        return mousedelta.y * vault.Get("sens") * Time.deltaTime;

    }

    public float xCamRot() {
        return mousedelta.x * vault.Get("sens") * Time.deltaTime;
        
    }

    public void SetMouseInput(Vector2 mouseinput) {
        mousedelta = new Vector2(mouseinput.x, mouseinput.y);
        yaxis += xCamRot();
        xaxis -= yCamRot();
        xaxis = Mathf.Clamp(xaxis, -20f, 20f);
        CamRotation();
        //Debug.Log(mousedelta);
    }

    public void CamRotation() {
        camHolder.rotation = Quaternion.Euler(xaxis, yaxis, 0f);
        player.rotation = Quaternion.Euler(0f, yaxis, 0f);

    }

}
