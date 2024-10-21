using UnityEngine;

public class Dialog_hook : MonoBehaviour {    

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PXCharacterController>().DialogEnter(transform);
    }
}
