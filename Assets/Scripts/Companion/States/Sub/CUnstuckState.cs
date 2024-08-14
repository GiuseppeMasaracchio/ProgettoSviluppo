using System.Collections;
using UnityEngine;

public class CUnstuckState : CBaseState {
    public CUnstuckState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) { }
    public override void EnterState() {
        //Enter logic
        InitializeContext();
    }
    public override void UpdateState() {
        //Update logic        

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
    }
    public override void CheckSwitchStates() {
        //Switch logic
        
        if (Ctx.IsOperative && Ctx.IsMoving) {
            SwitchState(StateHandler.Move());
        } 
        else if (Ctx.IsOperative && Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }
        else if (Ctx.IsOperative && Ctx.IsFocusing) {
            SwitchState(StateHandler.Focus());
        }
        else if (Ctx.IsOperative && Ctx.IsTalking) {
            SwitchState(StateHandler.Talk());
        }
    }
    public void InitializeContext() {
        Ctx.IsMoving = false;
        Ctx.IsIdle = false;
        Ctx.IsFocusing = false;
        Ctx.IsTalking = false;
    }    
}
