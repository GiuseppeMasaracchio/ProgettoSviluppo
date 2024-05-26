using UnityEngine;

public class SceneLoad_hook : MonoBehaviour {
    [SerializeField] Scenes scene;
    [SerializeField] Cp point;
    private void OnTriggerEnter(Collider other) {
        ScenesManager.Instance.Switch(scene, point);
    }
}
