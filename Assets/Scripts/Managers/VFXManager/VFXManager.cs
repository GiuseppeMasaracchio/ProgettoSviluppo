using UnityEngine;

public class VFXManager : MonoBehaviour {
    public static VFXManager Instance { get; private set; }

    [SerializeField] GameObject[] playerVfx;
    [SerializeField] GameObject[] enemyVfx;
    [SerializeField] GameObject[] envVfx;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }          

    public void SpawnFixedVFX(PlayerVFX _vfx, Vector3 position, Quaternion rotation) { //OVERLOAD
        Instantiate(playerVfx[(int)_vfx], position, rotation);        
    }

    public void SpawnFixedVFX(EnemyVFX _vfx, Vector3 position, Quaternion rotation) { //OVERLOAD
        Instantiate(enemyVfx[(int)_vfx], position, rotation);
    }

    public void SpawnFixedVFX(EnvVFX _vfx, Vector3 position, Quaternion rotation) { //OVERLOAD
        Instantiate(envVfx[(int)_vfx], position, rotation);
    }

    public void SpawnFollowVFX(PlayerVFX _vfx, Vector3 position, Quaternion rotation, GameObject parent) { //OVERLOAD
        GameObject _prefab = Instantiate(playerVfx[(int)_vfx], position, rotation);
        var script = _prefab.GetComponent<VFXBehaviour>();
        script.Parent = parent;
    }

    public void SpawnFollowVFX(EnemyVFX _vfx, Vector3 position, Quaternion rotation, GameObject parent) { //OVERLOAD
        GameObject _prefab = Instantiate(enemyVfx[(int)_vfx], position, rotation);
        var script = _prefab.GetComponent<VFXBehaviour>();
        script.Parent = parent;
    }

    public void SpawnFollowVFX(EnvVFX _vfx, Vector3 position, Quaternion rotation, GameObject parent) { //OVERLOAD
        GameObject _prefab = Instantiate(envVfx[(int)_vfx], position, rotation);
        var script = _prefab.GetComponent<VFXBehaviour>();
        script.Parent = parent;
    }

}
