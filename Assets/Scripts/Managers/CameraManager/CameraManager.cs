using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour {
    public static CameraManager Instance { get; private set; }

    [SerializeField] GameObject gameBrain;
    [SerializeField] GameObject menuBrain;

    private GameObject currentBrain;

    private List<GameObject> menuVCameras = new List<GameObject>();


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        currentBrain = gameBrain;
        currentBrain.SetActive(true);
    }

    private void Start() {
        //InitializeVCameras();
    }

    public void OnPauseCamera(InputAction.CallbackContext input) {
        if (input.phase == InputActionPhase.Started) {
            //menuVCameras[(int)MenuVCameras.PauseStart].gameObject.SetActive(true);
            //SwitchBrain(menuBrain);
            StartCoroutine("PauseCameraOut");
        }
    }

    public void OnResumeCamera(InputAction.CallbackContext input) {
        if (input.phase == InputActionPhase.Started) {
            StartCoroutine("PauseCameraIn");
            //SwitchBrain(gameBrain);
        }
    }

    public void SetMenuBrain() {
        SwitchBrain(menuBrain);
    }

    public void SetGameBrain() {
        SwitchBrain(gameBrain);
    }

    public void InitializeVCameras() {
        StartCoroutine("RetrieveVCameras");
    }

    private void SwitchBrain(GameObject targetBrain) {
        if (targetBrain != currentBrain) {
            targetBrain.SetActive(true);
            currentBrain.SetActive(false);
            currentBrain = targetBrain;
        }
    }

    private void SwitchVCamera(GameObject target) {

    }

    private IEnumerator PauseCameraOut() {
        Time.timeScale = 0f;
        InputManager.Instance.SetActionMap();

        if (!menuVCameras[(int)MenuVCameras.PauseStart].gameObject.activeSelf) {
            menuVCameras[(int)MenuVCameras.PauseStart].gameObject.SetActive(true);
        }

        SwitchBrain(menuBrain);
        yield return null;

        menuVCameras[(int)MenuVCameras.PauseEnd].gameObject.SetActive(true);
        menuVCameras[(int)MenuVCameras.PauseStart].gameObject.SetActive(false);
        
        yield break;
    }

    private IEnumerator PauseCameraIn() {
        menuVCameras[(int)MenuVCameras.PauseStart].gameObject.SetActive(true);
        menuVCameras[(int)MenuVCameras.PauseEnd].gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(.25f);

        SwitchBrain(gameBrain);
        InputManager.Instance.SetActionMap();
        Time.timeScale = 1f;

        yield break;
    }

    private IEnumerator RetrieveVCameras() {
        GameObject parent = GameObject.Find("VCameras");
        Cinemachine.CinemachineVirtualCamera[] cameras = parent.GetComponentsInChildren<Cinemachine.CinemachineVirtualCamera>();

        foreach (Cinemachine.CinemachineVirtualCamera camera in cameras) {
            menuVCameras.Add(camera.gameObject);
            camera.gameObject.SetActive(false);
            yield return null;
        }

        yield break;
    }
}