using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
<<<<<<< HEAD
=======
    private Vector2 screenPosition = Vector2.zero;
    private Vector3 view;
>>>>>>> bd7e1ba5bb6ba5198a5e8a19e9acda2db2b8b916
    private Transform camHolder;
    private Vault vault;

    void Awake() {
        camHolder = GameObject.Find("CameraHolder").transform;
<<<<<<< HEAD
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    public void ScreenPosition(Vector2 input) {
        camHolder.eulerAngles += new Vector3 (input.y, input.x, 0f) * Time.deltaTime * 60f;
        
=======
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

>>>>>>> bd7e1ba5bb6ba5198a5e8a19e9acda2db2b8b916
    }
}
        
