using System.Collections;
using UnityEngine;

public class CMoveState : CBaseState {
    public CMoveState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) { }
    public override void EnterState() {
        //Enter logic

        Ctx.StartCoroutine("TravelMoveRoutine");
        Ctx.VisionEnterMoveBehaviour();
        //SetVFXDrag(1f);

        InitializeContext();
    }
    public override void UpdateState() {
        //Update logic        

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        Ctx.VisionExitMoveBehaviour();
        //SetVFXDrag(0f);
    }
    public override void CheckSwitchStates() {
        //Switch logic
        
        if (Ctx.IsTalking) {
            SwitchState(StateHandler.Talk());
        } 
        else if (Ctx.IsFocusing) {
            SwitchState(StateHandler.Focus());
        }
        else if (Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }
        else if (Ctx.IsStuck && Ctx.IsUnstucking) {
            SwitchState(StateHandler.Unstuck());
        }
    }
    public void InitializeContext() {
        Ctx.IsTalking = false;
        Ctx.IsFocusing = false;
        Ctx.IsIdle = false;
        Ctx.IsUnstucking = false;
    }

    public void SetVFXDrag(float value) {
        Ctx.Trail.SetFloat("DragDirection", value);
    }
}
