using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour {
    [SerializeField] GameObject _vfx;

    void Awake() {
        Instantiate(_vfx, this.transform.position, this.transform.rotation);
    }

    // Start is called before the first frame update
    void Start() {
        _vfx.GetComponent<VisualEffect>().Play();

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(_vfx.GetComponent<VisualEffect>().GetSpawnSystemInfo(0).vfxEventAttribute.GetFloat("lifetime"));
    }
}
