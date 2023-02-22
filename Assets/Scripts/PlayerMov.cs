using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour {
    [SerializeField] Transform camHolder;
    [SerializeField] float horizontalSens;
    [SerializeField] Rigidbody player;
    //[SerializeField] PlayerInput puzzo;

    public CameraRotation cam;
    public Movement move;

    // Start is called before the first frame update

    void Awake() {
        cam = camHolder.GetComponent<CameraRotation>();
        move = player.GetComponent<Movement>();

    }

    void Start() {
        //Cursor.lockState = CursorLockMode.Confined;
        
        //camHolder.transform.rotation.Set(0f, 0f, 0f, 0f);
        
    }

    // Update is called once per frame
    void Update() {
        /*
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            puzzo.SwitchCurrentActionMap("UI");
            Debug.Log(puzzo.currentActionMap.name);

        }
        */
        //Debug.Log(camHolder.rotation);
        //Debug.Log(camHolder.localRotation);
        camHolder.rotation = cam.CamRot(horizontalSens);
        player.AddForce(move.Mov().normalized * 500f * Time.fixedDeltaTime);
        
    }
}
