using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private Vector2 screenPosition = Vector2.zero;
    private Vector3 view;
    private Transform camHolder;
    private Vault vault;

    void Awake() {
        camHolder = GameObject.Find("CameraHolder").transform;
        vault = GameObject.Find("ScriptsHolder").GetComponent<Vault>();
    }
    public float yCamRot() {
        return screenPosition.y * vault.Get("sens") * Time.fixedDeltaTime;
        //return Quaternion.Euler(Mathf.Clamp(screenPosition.y * vault.Get("sens"), -1000f, 2000f) * Time.fixedDeltaTime, 0f, 0f);
    }

    public float xCamRot() {
        return screenPosition.x * vault.Get("sens") * Time.fixedDeltaTime;
        //return Quaternion.Euler(0f, screenPosition.x * vault.Get("sens") * Time.fixedDeltaTime, 0f);
    }

    public void ScreenPosition(Vector2 mousePosition) {
        screenPosition = mousePosition;
        view = new Vector3(yCamRot(), xCamRot(), 0f);
        camHolder.eulerAngles += view;

    }
}
