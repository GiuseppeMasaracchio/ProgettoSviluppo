//Devo capre cosa togliere senza fare danno soz

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuScript : MonoBehaviour
{
    //References
    public AudioMixer mixer;
    public TMP_Dropdown resDrop;
    public GameObject modal;
    public PlayerInput pgInputMap; 

    
    //Variables
    private Resolution[] res;
    private bool isPaused = false;



    private void Awake() {
        res = Screen.resolutions;
        SetResolutionDrop();

        //if (GameMaster.Instance.GetSceneIndex() != 0) pgInputMap = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    public void SetResolutionDrop() {
        //provvisorio
        if (resDrop == null) return;
        
        
        resDrop.ClearOptions();
        List<string> temp = new List<string>();
        for (int i = 0; i < res.Length; i++) {
            temp.Add(res[i].width + " x " + res[i].height);
        }
        resDrop.AddOptions(temp);
    }

    public void SetVolume(float vol) {
        if (mixer == null) return;
        mixer.SetFloat("volume", vol);
    }

    public void SetQuality (int qualityIndex) { 
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool full) {
        Screen.fullScreen = full;
    }

    public void OptionsModal() {
        //if (GameMaster.Instance.GetSceneIndex() == 0) GetComponentInChildren<TMP_Text>().text = "OPTIONS";
        Pause();
    }


    public void Pause() {
        Image background = modal.GetComponentInParent<Image>();
        
        if (!isPaused) {
            if(pgInputMap != null) {
                pgInputMap.SwitchCurrentActionMap("UI");
                Cursor.lockState = CursorLockMode.None;
            }

            background.enabled = true;

            isPaused = true;

            modal.SetActive(true);

            //Funzione per stoppare il tempo
            Time.timeScale = 0f;

        } else {
            if (pgInputMap != null) {
                pgInputMap.SwitchCurrentActionMap("Player");
                Cursor.lockState = CursorLockMode.Locked;
            }

            background.enabled = false;

            isPaused = false;

            modal.SetActive(false);

            Time.timeScale = 1f; // >0 per lo slow motion, 1 per portarlo alla normalità
        }
    }


    public void QuitButton() {
        Debug.Log("Uscendo");
        Application.Quit();
    }

    public void PlayButton() {
        
    }
}
