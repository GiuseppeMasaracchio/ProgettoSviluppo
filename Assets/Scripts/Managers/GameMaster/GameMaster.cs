using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    public static GameMaster Instance { get; private set; }
    
    [SerializeField] PlayerInfo _playerinfo;


    private void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Game Master Start");

        ScenesManager.Instance.MainMenu();
        //StartCoroutine(StartGame());
    }

    private IEnumerator StartGame() {
        yield return new WaitForSeconds(10f);
        ScenesManager.Instance.StartGame();
        CameraManager.Instance.StartGame();

        while (ScenesManager.Instance.IsBusy()) {
            yield return null;
        }

        InputManager.Instance.StartGame();

        yield break;
    }
}
