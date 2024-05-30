using UnityEngine;
using UnityEngine.VFX;
public class VFXManager : MonoBehaviour {
    [SerializeField] VFX entityType;

    [Space]
    [SerializeField] GameObject[] _playerVfx;
    [SerializeField] GameObject[] _enemyVfx;
    [SerializeField] GameObject[] _objectVfx;

    void Awake() {
        
    }

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("InitalizePrefab", 1f, 1f);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void InitalizePrefab() {
        Instantiate(_playerVfx[0], this.transform.position, this.transform.rotation);
    }
}
