using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    //Local Variables
    private Vector2 mousedelta = Vector2.zero;
    private float xaxis;
    private float yaxis;

    //Scripts reference
    private Transform camholder;
    private Transform assetforward;
    private Transform player;
    private Vault vault;

    void Awake() {
        //Initialize scripts
        camholder = GameObject.Find("CameraHolder").transform;
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        assetforward = GameObject.Find("PlayerForward").transform;
        player = GameObject.Find("Player").transform;
    }

    void Update() {
        camholder.position = player.position;    
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
        xaxis = Mathf.Clamp(xaxis, -20f, 80f);
        CamRotation();
       
    }

    public void CamRotation() {
        camholder.rotation = Quaternion.Euler(xaxis, yaxis, 0f);
        assetforward.rotation = Quaternion.Euler(0f, yaxis, 0f);
        //player.forward = assetforward.forward;
    }


    public void SetForwardAxis() {
        player.forward = assetforward.forward;
        //player.rotation = Quaternion.Euler(0f, yaxis, 0f);
        //camHolder.rotation.Set(0f, 0f, 0f, 0f);
        //assetforward.rotation.Set(0f, 0f, 0f, 0f);
    }
}
