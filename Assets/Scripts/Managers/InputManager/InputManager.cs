using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    [SerializeField] PlayerInput playerInput;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else { Destroy(gameObject); }
    }
    private void Start() {

    }
    public PlayerInput GetPlayerInput() {
        return playerInput;
    }
    public void SwitchActionMap(InputAction.CallbackContext input) {
        if (input.phase == InputActionPhase.Started) {
            SetActionMap();
        }

    }

    private void SetActionMap() {
        if (playerInput.currentActionMap.name == "Player") {
            Time.timeScale = 0f;
            playerInput.SwitchCurrentActionMap("UI");
        } else {
            Time.timeScale = 1f;
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

}