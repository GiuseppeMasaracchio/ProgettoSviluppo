using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene {
    Lab,
    Map1,
    Warp1,
    Warp2,
    Warp3
}

public class ScenesManager : MonoBehaviour {
    public static ScenesManager Instance { get; private set; }
    
    [SerializeField] Material _fade;
    
    [SerializeField]
    [Range(.1f, 2f)] float _fadeSpeed;

    private bool paused;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else { Destroy(this); }
    }

    public void Load(Scene scene) {
        int n_scene = (int)scene;
        if (n_scene != SceneManager.GetActiveScene().buildIndex) {
            StartCoroutine(InitializeSwitch(n_scene));
        }
    }

    public void ReloadOnDeath() {
        StartCoroutine(OnDeath());
    }

    private IEnumerator InitializeSwitch(int n_scene) {
        paused = true;
        StartCoroutine(FadeIn());
        yield return new WaitWhile(() => paused);
        SceneManager.LoadScene(n_scene, LoadSceneMode.Single);
        yield return new WaitForSeconds(.2f);
        StartCoroutine(FadeOut());
        
        yield break;
    }

    private IEnumerator FadeIn() {
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
        while (_fade.GetFloat("_Size") < 3f) {
            float size = _fade.GetFloat("_Size");
            float lerpSize = size + _fadeSpeed * .1f;
            _fade.SetFloat("_Size", lerpSize);
            yield return null;
        }

        _fade.SetFloat("_Size", 3f);
        Debug.Log("Fade out");
        yield break;
    }

    public IEnumerator OnDeath() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(InitializeSwitch(SceneManager.GetActiveScene().buildIndex));
    }
}
 