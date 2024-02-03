using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    //private enum AudioClips {
    //    mainTheme = 0,
    //    jumpSfx = 1,
    //}
    //[SerializeField] AudioClip[] soundClips;

    [SerializeField] AudioSource _musicSource, _sfxSource;
    public AudioClip jumpSfx { private set; get; }
    public AudioClip mainTheme { private set; get; }

    public static AudioManager _audioManagerInstance;
    public AudioMixer _mixer;

    private void Awake() {
        if (_audioManagerInstance == null) _audioManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartMusic() {
        _musicSource.clip = mainTheme;
        _musicSource.Play();
    }

    //Metodo esterno
    public void StartSfx(AudioClip clip) {
        _sfxSource.PlayOneShot(clip);
    }

}

//Prova chiamata esterna
//AudioManager._audioManagerInstance.StartSfx(AudioManager._audioManagerInstance.jumpSfx);
