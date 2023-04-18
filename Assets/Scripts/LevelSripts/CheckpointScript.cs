using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckpointScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) Vault.SetCheckpoint(transform.position);
    }
}
