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


    //Variables
    private Resolution[] res;



    private void Start() {
        mixer = FindObjectOfType<AudioMixer>();
        res = Screen.resolutions;
        SetResolutionDrop();
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

    public void OptionButton() {
        StartCoroutine(OpenModal());   
    }

    private IEnumerator OpenModal() {
        if (modal != null) modal.SetActive(true);

        yield return null;
    }

    public void BackButton() {
        modal.SetActive(false);
        
        StopCoroutine(OpenModal());
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
