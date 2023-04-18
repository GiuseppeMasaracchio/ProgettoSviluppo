using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster _instance;
    public static GameMaster Instance { get { return _instance; } }
    public CheckpointManager CPM_Instance { get; private set; }

    private Animator anim;
    [SerializeField]private GameObject pgSpawn;

    private int levelIndex;

    private void Awake() {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        
        //provvisorio
        if (SceneManager.GetActiveScene().buildIndex != 0) pgSpawn = GameObject.Find("Player_spawnpoint");
        anim = GetComponent<Animator>();

        CPM_Instance = GetComponent<CheckpointManager>();
    }

  
    public void Respawn() {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangeLevel(int index) {
        levelIndex = index;
        anim.SetTrigger("FadeOut");
    }

    public int GetSceneIndex() {
        return levelIndex;
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(levelIndex);

        
    }

    
}
