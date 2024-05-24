using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps_hook : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        _playerInfo.PowerUps++;

        Destroy(gameObject);
    }
}
