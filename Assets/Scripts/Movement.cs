using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;
    public Vector3 Move() {
        return new Vector3 (direction.x, 0f, direction.y);
    }

    public void Direction(Vector2 dir) {
        this.direction = dir;
    }
}
