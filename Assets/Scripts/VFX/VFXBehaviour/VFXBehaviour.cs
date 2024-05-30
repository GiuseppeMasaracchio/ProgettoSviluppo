using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXBehaviour : MonoBehaviour {

    [Range(0.1f, 5f)]
    [SerializeField] public float lifeTime;

    private VisualEffect _vfx;

    void Awake() {
        _vfx = this.GetComponent<VisualEffect>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBehaviour());
    }

    private IEnumerator StartBehaviour() {
        _vfx.Play();
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
        yield break;
    }
}
