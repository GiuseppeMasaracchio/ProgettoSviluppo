using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    public Vector2 Rotation = Vector2.zero;
    
    public Quaternion CamRot(float horizontalSens) {
        //Devo riempire questa funzione per calcolare manualmente l'angolo di rotazione e restituirlo
        return Quaternion.Euler(Mathf.Clamp(Rotation.y * horizontalSens, -1000f, 2000f) * Time.fixedDeltaTime, Rotation.x * horizontalSens * Time.fixedDeltaTime, 0f);
    }

    public void xRot(Vector2 _xRotation) {
        this.Rotation = _xRotation;

    }
}
