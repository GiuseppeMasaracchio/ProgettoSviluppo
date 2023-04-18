using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) GameMaster.Instance.Respawn();
        else Destroy(other.gameObject);
    }
}
