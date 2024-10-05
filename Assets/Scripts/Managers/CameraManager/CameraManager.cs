using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance { get; private set; }

    [SerializeField] GameObject gameBrain;
    [SerializeField] GameObject menuBrain;

    private GameObject currentBrain;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        currentBrain = gameBrain;
        currentBrain.SetActive(true);
    }

    public void OnPauseCamera(InputAction.CallbackContext input) {
        if (input.phase == InputActionPhase.Started) {            
            SwitchBrain(menuBrain);
        }
    }

    public void OnResumeCamera(InputAction.CallbackContext input) {
        if (input.phase == InputActionPhase.Started) {
            SwitchBrain(gameBrain);
        }
    }

    public void SwitchBrain(GameObject targetBrain) {
        if (targetBrain != currentBrain) {
            targetBrain.SetActive(true);
            currentBrain.SetActive(false);
            currentBrain = targetBrain;
        }
    }
}