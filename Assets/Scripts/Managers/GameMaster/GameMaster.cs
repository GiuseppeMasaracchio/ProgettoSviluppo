using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    [SerializeField] PlayerInfo _playerinfo;
    public static GameMaster Instance { get; private set; }

    private void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Game Master Start");

        InitializePlayerInfo();
        ScenesManager.Instance.Starter(Scenes.Lab);
    }

    // Update is called once per frame
    void Update() {

    }

    private void InitializePlayerInfo() {
        _playerinfo.Checkpoint = new Vector2(1f, 1f);
        _playerinfo.CurrentHp = 3;
        _playerinfo.PowerUps = 2;
        _playerinfo.Score = 0;
    }

}