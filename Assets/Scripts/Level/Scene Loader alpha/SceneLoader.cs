using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [SerializeField]
    public int sceneIdx;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Single);        
    }
}
