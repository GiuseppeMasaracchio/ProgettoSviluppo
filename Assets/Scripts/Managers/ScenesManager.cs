using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes {
    Lab,
    Map1,
    Warp1,
    Warp2,
    Warp3
}
public enum Cp {
    CP_4,
    CP_3,
    CP_2,
    CP_1,
    CP_0
}

public class ScenesManager : MonoBehaviour {
    public static ScenesManager Instance { get; private set; }
    
    [SerializeField] Material _fade;

    [SerializeField]
    [Range(.1f, 2f)] float _fadeSpeed;

    private bool paused;
    private GameObject[] _dir;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else { Destroy(this); }
    }

    public void Load(Scenes scene) {
        if (paused) { return; }

        int n_scene = (int)scene;
        if (n_scene != SceneManager.GetActiveScene().buildIndex) {
            StartCoroutine(InitializeSwitch(n_scene));
        }
    }

    public void ReloadOnDeath() {
        StartCoroutine(OnDeath());
    }    

    private IEnumerator InitializeSwitch(int n_scene) {
        StartCoroutine(FadeIn());
        yield return new WaitWhile(() => paused);

        SceneManager.LoadScene(n_scene, LoadSceneMode.Single);        

        StartCoroutine(RetrieveCheckpoints());
        yield return new WaitWhile(() => paused);

        StartCoroutine(FadeOut());
        yield return new WaitWhile(() => paused);

        yield break;
    }

    public IEnumerator OnDeath() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(InitializeSwitch(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator FadeIn() {
        paused = true;
        while (_fade.GetFloat("_Size") > 0.01f) {
            float size = _fade.GetFloat("_Size");
            float lerpSize = size - _fadeSpeed * .1f;
            _fade.SetFloat("_Size", lerpSize);            
            yield return null;
        }

        _fade.SetFloat("_Size", 0f);
        paused = false;
        Debug.Log("Fade in");
        yield break;
    }

    private IEnumerator FadeOut() {
        paused = true;
        while (_fade.GetFloat("_Size") < 3f) {
            float size = _fade.GetFloat("_Size");
            float lerpSize = size + _fadeSpeed * .1f;
            _fade.SetFloat("_Size", lerpSize);
            yield return null;
        }

        _fade.SetFloat("_Size", 3f);
        paused = false;
        Debug.Log("Fade out");
        yield break;
    }

    public IEnumerator RetrieveCheckpoints() {
        paused = true;
        yield return new WaitForSeconds(.2f);

        _dir = GameObject.FindGameObjectsWithTag("Checkpoint");        

        yield return new WaitForSeconds(.2f);

        paused = false;
        yield break;
    }
}
 