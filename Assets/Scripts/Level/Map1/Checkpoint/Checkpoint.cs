using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private Vector3 startPos;
    private Vector3 lowPos;
    private Vector3 highPos;
    private Vector3 position;
    private Vector3 yShift = new Vector3(0, .25f, 0);

    private float lowDistance;
    private float highDistance;

    private float lerpValue = .01f;

    private int dir = 1; //1 is Up //-1 is Down

    void Awake() {
        startPos = this.transform.position;
        lowPos = startPos - yShift;
        highPos = startPos + yShift;
    }

    void Update() {
        GetPosition();
        SetPosition();
    }
    private void GetPosition() {
        position = this.transform.position;
        lowDistance = Vector3.Distance(position, lowPos);
        highDistance = Vector3.Distance(position, highPos);
    }
    private void SetPosition() {
        if (dir == 1) {
            LowToHigh();
        } else if (dir == -1) {
            HighToLow();
        }
    }
    private void LowToHigh() {
        if (highDistance <= .05f) {
            dir = -1;
            return;
        } else {
            Vector3 lerpPosition = Vector3.Lerp(position, highPos, lerpValue + Time.deltaTime);
            this.transform.position = lerpPosition;
        }
    }
    private void HighToLow() {
        if (lowDistance <= .05f) {
            dir = 1;
            return;
        }
        else {
            Vector3 lerpPosition = Vector3.Lerp(position, lowPos, lerpValue + Time.deltaTime);
            this.transform.position = lerpPosition;
        }
    }
}
