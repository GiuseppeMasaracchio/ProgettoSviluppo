using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    void OnEnable() {
        EventManager.OnAction += prova;
    }

    private void OnDisable() {
        EventManager.OnAction -= prova;
    }
    void prova() {
        Debug.Log("Eventato");
    }
}
