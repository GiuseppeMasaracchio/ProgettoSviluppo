using UnityEngine;
using UnityEngine.VFX;

public class Trail : MonoBehaviour {
    [SerializeField] Transform asset;
    [SerializeField] GameObject vfx;

    private VisualEffect effect;
    private TPCharacterController ctx;
    void Awake() {
        effect = vfx.GetComponent<VisualEffect>();
        ctx = asset.GetComponent<TPCharacterController>();
    }
    void Update() {
        effect.SetVector2("RefDirection", ctx.MoveInput);
    }
}
