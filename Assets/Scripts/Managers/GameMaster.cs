using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    [SerializeField]
    public GameObject _checkpoint;
    public GameObject _player;
    public int id;
    public static GameMaster Instance { get; private set; }

    private void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
            Debug.Log("Instance created. " + this.gameObject.name);
        }
        else {
            Debug.Log("Instance existing already, destroying this. " + this.gameObject.name);
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Start");
        StartCoroutine(SetCP());       
    }

    // Update is called once per frame
    void Update() {

    }

    public IEnumerator SetCP() {
        yield return new WaitForSeconds(2f);
        _player.transform.position = _checkpoint.transform.position;
        _player.transform.forward = _checkpoint.transform.forward;
        yield break;
    }
}
