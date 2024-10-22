using UnityEngine;

public class DialogController : MonoBehaviour {
    [SerializeField] GameObject _vcam;
    [SerializeField] GameObject _focusTarget;
    [SerializeField] GameObject _playerTarget;
    [SerializeField] GameObject _companionTarget;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        PXCharacterController _ctx = _player.GetComponent<PXCharacterController>();
        _ctx.DialogEnter(_playerTarget.transform, _focusTarget.transform, _vcam);
    }
}
