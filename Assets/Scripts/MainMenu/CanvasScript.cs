using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMasterVolume(float val) {
        _mixer.SetFloat("Master", val);
    }
    public void SetMusicVolume(float val) {
        _mixer.SetFloat("Music", val);
    }
    public void SetEffectsVolume(float val) {
        _mixer.SetFloat("Effects", val);
    }
}
