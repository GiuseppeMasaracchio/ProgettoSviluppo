using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    public static ScenesManager Instance { get; private set; }
    
    [SerializeField] Material _fade;
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _companionPrefab;
    [SerializeField] PlayerInfo _playerInfo;

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

    public void StartGame() {
        if (paused) { return; }

        StartCoroutine(InitializeStarter((int)Scenes.Lab));        
    }

    public void Switch(Scenes scene, Cp point) {
        if (paused) { return; }

        int n_scene = (int)scene;
        if (n_scene != SceneManager.GetActiveScene().buildIndex) {
            StartCoroutine(InitializeSwitch(n_scene, point));
        }
    }

    public void LoadGame(Scenes scene) {
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

        StartCoroutine(InstantiatePlayerAndCompanion());
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

        StartCoroutine(InstantiatePlayerAndCompanion());
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

        StartCoroutine(InstantiatePlayerAndCompanion());
        yield return new WaitWhile(() => paused);

        StartCoroutine(FadeOut());
        yield return new WaitWhile(() => paused);

        yield break;
    }

    public IEnumerator OnDeath() {
        yield return new WaitForSeconds(2f);

        StartCoroutine(InitializeLoad(SceneManager.GetActiveScene().buildIndex));
        yield return null;

        _playerInfo.CurrentHp = 3;

        yield break;
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

        if (SceneManager.GetActiveScene().buildIndex == _playerInfo.Checkpoint.x) {
            _targetCp = (Cp)_playerInfo.Checkpoint.y;
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

    public IEnumerator InstantiatePlayerAndCompanion() {
        paused = true;
        Instantiate(_playerPrefab, _cpObj.transform.position, _cpObj.transform.rotation);
        yield return new WaitForSeconds(.2f);

        Instantiate(_companionPrefab, _cpObj.transform.position + new Vector3(0f, 1f, 1f), _cpObj.transform.rotation);
        yield return new WaitForSeconds(.2f);

        paused = false;
        yield break;
    }

}
 