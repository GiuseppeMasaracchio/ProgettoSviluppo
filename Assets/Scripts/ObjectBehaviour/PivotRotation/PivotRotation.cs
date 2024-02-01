using UnityEngine;

public class PivotRotation : MonoBehaviour {
    [SerializeField]
    [Range(0.01f, 2f)] float speed;
    void Update() {
        this.transform.Rotate(Vector3.up * speed);
    }
}
