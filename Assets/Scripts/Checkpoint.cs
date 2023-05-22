using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Checkpoint
{
    private static Vector3 checkpoint;

    public static Vector3 GetCheckpoint() {
        return checkpoint;
    }

    public static void SetCheckPoint(Vector3 newcheckpoint) {
        checkpoint = newcheckpoint;
    }
}
