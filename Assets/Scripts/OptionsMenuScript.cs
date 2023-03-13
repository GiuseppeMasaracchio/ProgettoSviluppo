//Devo capre cosa togliere senza fare danno soz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;
using Unity.VisualScripting;


public class OptionsMenuScript : MonoBehaviour
{
    //References
    public AudioMixer mixer;
    public TMP_Dropdown resDrop;
    public GameObject modal;

    //Temp
    //private InputActionAsset pgActionMaps;
    
    //Variables
    private Resolution[] res;
    private bool isPaused = false;



    private void Start() {
        mixer = FindObjectOfType<AudioMixer>();
        res = Screen.resolutions;
        SetResolutionDrop();


        //pgActionMaps = GetComponent<InputActionAsset>();
    }

    public void SetResolutionDrop() {
        //provvisorio
        if (resDrop == null) return;
        
        
        resDrop.ClearOptions();
        List<string> temp = new List<string>();
        for (int i = 0; i < res.Length; i++) {
            string pesce = res[i].width + " x " + res[i].height;
            temp.Add(pesce);

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
        Pause();
    }


    public void Pause() {
        if (!isPaused) {

            isPaused = true;
            
            //if (modal != null) modal.SetActive(isPaused);
            modal.SetActive(true);

            //Funzione per stoppare il tempo
            Time.timeScale = 0f;

        } else {
            isPaused = false;

            modal.SetActive(isPaused);

            Time.timeScale = 1f; // >0 per lo slow motion, 1 per portarlo alla normalità
        }
    }


    public void QuitButton() {
        Debug.Log("Uscendo");
        Application.Quit();
    }

    public void PlayButton() {
        //To do impostare scene
        //SceneManager.LoadScene(1);
    }
}
