using TMPro;
using UnityEngine;

public class DevToolUI : MonoBehaviour {
    private TMP_Text _rootText;
    private TMP_Text _subText;
    private TMP_Text _jumpCountText;
    private TMP_Text _canDashText;
    private TMP_Text _gravityText;
    private TMP_Text _sensText;
    private TMP_Text _currentClip;
    private TMP_Text _targetClip;

    void Awake() {
        _rootText = GameObject.Find("RootState").GetComponent<TMP_Text>();
        _subText = GameObject.Find("SubState").GetComponent<TMP_Text>();
        _jumpCountText = GameObject.Find("JumpCount").GetComponent<TMP_Text>();
        _canDashText = GameObject.Find("CanDash").GetComponent<TMP_Text>();
        _gravityText = GameObject.Find("Gravity").GetComponent<TMP_Text>();
        _sensText = GameObject.Find("CurrentSens").GetComponent<TMP_Text>();
        _currentClip = GameObject.Find("CurrentClip").GetComponent<TMP_Text>();
        _targetClip = GameObject.Find("TargetClip").GetComponent<TMP_Text>();
    }
    public void UpdateText(PXController _ctx) {
        _rootText.text = _ctx.CurrentRootState.ToString();
        _subText.text = _ctx.CurrentSubState.ToString();
        _jumpCountText.text = _ctx.JumpCount.ToString();
        _canDashText.text = _ctx.CanDash.ToString();
        _gravityText.text = _ctx.Gravity.ToString();
        _sensText.text = _ctx.CurrentSens.ToString();
        _currentClip.text = _ctx.AnimHandler.CurrentClip.ToString();
        _targetClip.text = _ctx.AnimHandler.TargetClip.ToString();
    }
}
