using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {


    private Vector2 screenPosition = Vector2.zero;
    private Vector3 view;

    private Vector2 mousedelta = Vector2.zero;
    private float xaxis;
    private float yaxis;


    //Scripts reference
    private Transform camholder;
    private Transform assetforward;
    private Transform player;
    //private Vault vault;

    void Awake() {
        //Initialize scripts
        camholder = GameObject.Find("CameraHolder").transform;
        //vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
        assetforward = GameObject.Find("PlayerForward").transform;
        player = GameObject.Find("Player").transform;
    }

    void Update() {
        camholder.position = player.position;
        yaxis += xCamRot();
        xaxis -= yCamRot();
        xaxis = Mathf.Clamp(xaxis, -20f, 80f);
        CamRotation();
    }

    public float yCamRot() {
        return mousedelta.y * Vault.Get("sens") * Time.deltaTime;

    }

    public float xCamRot() {
        return mousedelta.x * Vault.Get("sens") * Time.deltaTime;
        
    }

    public void SetMouseInput(Vector2 mouseinput) {
        mousedelta = new Vector2(mouseinput.x, mouseinput.y);
        //yaxis += xCamRot();
        //xaxis -= yCamRot();
        //xaxis = Mathf.Clamp(xaxis, -20f, 80f);
        //CamRotation();
       
    }

    public void CamRotation() {
        camholder.rotation = Quaternion.Euler(xaxis, yaxis, 0f);
        assetforward.rotation = Quaternion.Euler(0f, yaxis, 0f);
    }
}
