using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mediator : IMediator
{
    public void  NotifyMediator(GameObject obj, string button) {

    }

    private enum buttons {
        play = 0,

    }

    public PlayerInput input;

    public Canvas pauseCanvas;
    public Canvas devCanvas;

    public void AttivaPausa() {
        devCanvas.enabled = false;
        input.SwitchCurrentActionMap("UI");
        pauseCanvas.enabled = true;
    }

    public void DisattivaPausa() {
        pauseCanvas.enabled = false;
        input.SwitchCurrentActionMap("Player");
        devCanvas.enabled = true;
    }
}
