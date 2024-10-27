using UnityEngine;
using UnityEngine.VFX;

public class Trail : MonoBehaviour {
    [SerializeField] Transform asset;
    [SerializeField] GameObject vfx;

    private VisualEffect effect;
    private PXCharacterController ctx;
    void Awake() {
        effect = vfx.GetComponent<VisualEffect>();
        ctx = asset.GetComponent<PXCharacterController>();
    }
    void Update() {
        effect.SetVector2("RefDirection", ctx.MoveInput);
    }
}
