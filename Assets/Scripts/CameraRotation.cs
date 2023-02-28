using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    private Transform camHolder;

    void Awake() {
        camHolder = GameObject.Find("CameraHolder").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    public void ScreenPosition(Vector2 input) {
        camHolder.eulerAngles += new Vector3 (input.y, input.x, 0f) * Time.deltaTime * 60f;
        
    }
}
        
