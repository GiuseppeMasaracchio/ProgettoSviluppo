using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    //Local Variables
    private Vector2 mousedelta = Vector2.zero;
    private float xaxis;
    private float yaxis;

    private float camycurrent;
    private float camytarget;
    private float camylerp;

    //Scripts reference
    private Transform camholder;
    private Transform assetforward;
    private Transform player;

    void Awake() {
        //Initialize scripts
        camholder = GameObject.Find("CameraHolder").transform;
        assetforward = GameObject.Find("PlayerForward").transform;
        player = GameObject.Find("Player").transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        VerticalSmoothCam();
        yaxis += xCamRot();
        xaxis -= yCamRot();
        xaxis = Mathf.Clamp(xaxis, -20f, 80f);
        CamRotation();
    }

    public void SetMouseInput(Vector2 mouseinput) {
        mousedelta = new Vector2(mouseinput.x, mouseinput.y);
       
    }

    private float yCamRot() {
        return mousedelta.y * Vault.Get("sens") * Time.deltaTime;

    }

    private float xCamRot() {
        return mousedelta.x * Vault.Get("sens") * Time.deltaTime;
        
    }


    private void CamRotation() {
        camholder.rotation = Quaternion.Euler(xaxis, yaxis, 0f);
        assetforward.rotation = Quaternion.Euler(0f, yaxis, 0f);
    }

    private void VerticalSmoothCam() {
        camycurrent = camholder.position.y;
        camytarget = player.position.y;
        camylerp = Mathf.Lerp(camycurrent, camytarget, .025f);
        if (camycurrent < camytarget) {
            camholder.position = new Vector3(player.position.x, camylerp, player.position.z);
        } else {
            camholder.position = player.position;
        }
    }
}
