using UnityEngine;
using UnityEngine.VFX;

public class Trail : MonoBehaviour {
    [SerializeField] Transform asset;
    [SerializeField] GameObject vfx;

    private VisualEffect effect;
    private PXController ctx;
    void Awake() {
        effect = vfx.GetComponent<VisualEffect>();
        ctx = asset.GetComponent<PXController>();
    }
    void Update() {
        effect.SetVector2("RefDirection", ctx.MoveInput);
    }
}
