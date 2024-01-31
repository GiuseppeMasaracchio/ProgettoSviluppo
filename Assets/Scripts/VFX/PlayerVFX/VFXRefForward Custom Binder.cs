using UnityEngine;
using UnityEngine.VFX;

public class VFXRefForwardCustomBinder : MonoBehaviour
{
    [SerializeField] Transform asset;
    [SerializeField] GameObject vfx;

    private VisualEffect effect;

    void Awake() {
        effect = vfx.GetComponent<VisualEffect>();

    }
    void Update() {
        effect.SetVector3("RefDirection", asset.forward);
    }
}
