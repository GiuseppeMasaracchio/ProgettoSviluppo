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
            //SetActionMap();
        }

    }

    public void OnAnyButton(InputAction.CallbackContext input) {
        if (input.phase == InputActionPhase.Started) {
            SetActionMap("UI");
        }
    }

    public void SetActionMap(string target) {
        if (playerInput.currentActionMap.name != target) {
            playerInput.SwitchCurrentActionMap(target);
        }        
    }
}