using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
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
    CP_0,
    CP_1,
    CP_2,
    CP_3,
    CP_4
}

public class ScenesManager : MonoBehaviour {
    public static ScenesManager Instance { get; private set; }
    
    [SerializeField] Material _fade;
    [SerializeField] GameObject _playerprefab;
    [SerializeField] PlayerInfo _playerinfo;

    [SerializeField]
    [Range(.1f, 2f)] float _fadeSpeed;

    private bool paused;

    private Cp _targetCp;
    private GameObject _cpObj;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else { Destroy(this); }
    }

    public void Starter(Scenes scene) {
        if (paused) { return; }

        StartCoroutine(InitializeStarter((int)scene));        
    }

    public void Switch(Scenes scene, Cp point) {
        if (paused) { return; }

        int n_scene = (int)scene;
        if (n_scene != SceneManager.GetActiveScene().buildIndex) {
            StartCoroutine(InitializeSwitch(n_scene, point));
        }
    }

    public void Load(Scenes scene) {
        if (paused) { return; }

        int n_scene = (int)scene;
        if (n_scene != SceneManager.GetActiveScene().buildIndex) {
            StartCoroutine(InitializeLoad(n_scene));
        }
    }

    public void ReloadOnDeath() {
        StartCoroutine(OnDeath());
    }

    private IEnumerator InitializeStarter(int n_scene) {
        StartCoroutine(BlackOut());
        yield return new WaitWhile(() => paused);

        SceneManager.LoadScene(n_scene, LoadSceneMode.Single);

        StartCoroutine(RetrievePoint(Cp.CP_0));
        yield return new WaitWhile(() => paused);

        StartCoroutine(InstantiatePlayer());
        yield return new WaitWhile(() => paused);

        StartCoroutine(FadeOut());
        yield return new WaitWhile(() => paused);

        yield break;
    }

    private IEnumerator InitializeLoad(int n_scene) {
        StartCoroutine(FadeIn());
        yield return new WaitWhile(() => paused);

        SceneManager.LoadScene(n_scene, LoadSceneMode.Single);

        StartCoroutine(RetrieveCheckpoints());
        yield return new WaitWhile(() => paused);

        StartCoroutine(InstantiatePlayer());
        yield return new WaitWhile(() => paused);

        StartCoroutine(FadeOut());
        yield return new WaitWhile(() => paused);

        yield break;
    }

    private IEnumerator InitializeSwitch(int n_scene, Cp _point) {
        StartCoroutine(FadeIn());
        yield return new WaitWhile(() => paused);

        SceneManager.LoadScene(n_scene, LoadSceneMode.Single); 

        StartCoroutine(RetrievePoint(_point));
        yield return new WaitWhile(() => paused);

        StartCoroutine(InstantiatePlayer());
        yield return new WaitWhile(() => paused);

        StartCoroutine(FadeOut());
        yield return new WaitWhile(() => paused);

        yield break;
    }

    public IEnumerator OnDeath() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(InitializeLoad(SceneManager.GetActiveScene().buildIndex));
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

        yield break;
    }

    private IEnumerator BlackOut() {
        paused = true;
        _fade.SetFloat("_Size", 0f);
        yield return null;

        paused = false;

        yield break;
    }

    public IEnumerator RetrieveCheckpoints() {
        paused = true;
        yield return new WaitForSeconds(.2f);

        GameObject[] _tmp = GameObject.FindGameObjectsWithTag("Checkpoint");

        if (SceneManager.GetActiveScene().buildIndex == _playerinfo.Checkpoint.x) {
            _targetCp = (Cp)_playerinfo.Checkpoint.y;
        } else { _targetCp = Cp.CP_0; }

        foreach (GameObject _obj in _tmp) {
            if (_obj.name == _targetCp.ToString()) {
                _cpObj = _obj;
                Debug.Log("Checkpoint: " + _cpObj.name + " Found! " + (int)_targetCp);
            }
        }

        yield return new WaitForSeconds(.2f);

        paused = false;
        yield break;
    }

    public IEnumerator RetrievePoint(Cp _point) {
        paused = true;
        yield return new WaitForSeconds(.2f);

        GameObject[] _tmp = GameObject.FindGameObjectsWithTag("Checkpoint");

        _targetCp = _point;        

        foreach (GameObject _obj in _tmp) {
            if (_obj.name == _targetCp.ToString()) {
                _cpObj = _obj;
                Debug.Log("Point: " + _cpObj.name + " Found! " + (int)_targetCp);
            }
        }

        yield return new WaitForSeconds(.2f);

        paused = false;
        yield break;
    }

    public IEnumerator InstantiatePlayer() {
        paused = true;
        Instantiate(_playerprefab, _cpObj.transform.position, _cpObj.transform.rotation);
        yield return new WaitForSeconds(.2f);

        paused = false;
        yield break;
    }

}
 