using UnityEngine;

public class InteractController : MonoBehaviour {
    [SerializeField] GameObject _vcam;
    [SerializeField] GameObject _focusTarget;
    [SerializeField] GameObject _playerTarget;
    [SerializeField] GameObject _companionTarget;
    [SerializeField] GameObject _popUp;

    public GameObject VCam { get { return _vcam; } set { _vcam = value; } }
    public GameObject FocusTarget { get { return _focusTarget; } set { _focusTarget = value; } }
    public GameObject PlayerTarget { get { return _playerTarget; } set { _playerTarget = value; } }
    public GameObject CompanionTarget { get { return _companionTarget; } set { _companionTarget = value; } }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _popUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            _popUp.SetActive(false);
        }
    }

    public void OnInteract() {
        _popUp.SetActive(false);
    }
}
