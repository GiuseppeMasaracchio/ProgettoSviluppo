using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour {
    void Update() {
        this.transform.Rotate(Vector3.up);
    }
}
