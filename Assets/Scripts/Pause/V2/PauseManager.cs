using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager _pmInstance;

    [SerializeField] private float pauseAnimTime = 0.2f; 
    private Image _overlayImg;
    private PlayerInput _playerInput;

    private void Awake() {
        if (_pmInstance == null) _pmInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _overlayImg = GetComponent<Image>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGamePause() {
        _playerInput.SwitchCurrentActionMap("UI");
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = Mathf.Lerp(1, 0f, pauseAnimTime);
        _overlayImg.color.WithAlpha(Mathf.Lerp(0f, 1f, pauseAnimTime));

        //Cam switch
    }

    private void OnGameResume() {
        //Cam switch

        _playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = Mathf.Lerp(0f, 1f, pauseAnimTime);
        _overlayImg.color.WithAlpha(Mathf.Lerp(1f, 0f, pauseAnimTime));
    }
}
