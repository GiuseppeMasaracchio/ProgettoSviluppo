using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager _instance;
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private bool isCheckpointActive = false;

    private void Awake() {
        DontDestroyOnLoad(this);
        if(Instance != null && Instance != this) {
            Destroy(this);
            return;
        }

        Instance = this;


        if (isCheckpointActive) ChangeSpawn();
    }

    public void ChangeSpawn() {
        Vector3 pgSpawn = GameObject.Find("Player_spawnpoint").transform.position;
        pgSpawn = Vault.GetCheckpoint();
    }

    public void ActivateCheckpoint(Transform input) {
        if (!isCheckpointActive) {
            isCheckpointActive = true;
            Vault.SetCheckpoint(input.position);
            input.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
