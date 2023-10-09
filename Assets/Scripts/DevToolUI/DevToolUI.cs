using TMPro;
using UnityEngine;

public class DevToolUI : MonoBehaviour {
    private TMP_Text _rootText;
    private TMP_Text _subText;
    private TMP_Text _jumpCountText;
    private TMP_Text _moveSpeedText;

    void Awake() {
        _rootText = GameObject.Find("RootState").GetComponent<TMP_Text>();
        _subText = GameObject.Find("SubState").GetComponent<TMP_Text>();
        _jumpCountText = GameObject.Find("JumpCount").GetComponent<TMP_Text>();
        _moveSpeedText = GameObject.Find("MoveSpeed").GetComponent<TMP_Text>();
    }
    public void UpdateText(TPCharacterController _ctx) {
        _rootText.text = _ctx.CurrentRootState.ToString();
        _subText.text = _ctx.CurrentSubState.ToString();
        _jumpCountText.text = _ctx.JumpCount.ToString();
        _moveSpeedText.text = _ctx.MoveSpeed.ToString();
    }
}
