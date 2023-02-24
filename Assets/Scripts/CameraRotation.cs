using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    public Vector2 screenPosition = Vector2.zero;
    [SerializeField] Transform camHolder;
    
    public Quaternion CamRot(float horizontalSens) {
        return Quaternion.Euler(Mathf.Clamp(screenPosition.y * horizontalSens, -1000f, 2000f) * Time.fixedDeltaTime, screenPosition.x * horizontalSens * Time.fixedDeltaTime, 0f);
    }

    public void ScreenPosition(Vector2 mousePosition) {
        this.screenPosition = mousePosition;
        camHolder.rotation = CamRot(20f);
    }
}
