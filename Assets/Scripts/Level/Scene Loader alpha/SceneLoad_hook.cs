using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad_hook : MonoBehaviour {
    [SerializeField] Scenes scene;
    private void OnTriggerEnter(Collider other) {
        ScenesManager.Instance.Load(scene);
    }
}
