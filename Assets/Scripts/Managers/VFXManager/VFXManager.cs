using UnityEngine;
using UnityEngine.VFX;
public class VFXManager : MonoBehaviour {
    public static VFXManager Instance { get; private set; }

    [SerializeField] GameObject[] playerVfx;
    [SerializeField] GameObject[] enemyVfx;
    [SerializeField] GameObject[] objectVfx;

    private VFX entityType;
    private Vector3 _targetPos;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("InitalizePrefab", 1f, 1f);
    }

    public void SpawnVFX(GameObject target) {
        _targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void InitalizePrefab() {
        Instantiate(playerVfx[0], this.transform.position, this.transform.rotation);
    }
}
