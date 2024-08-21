using System.Collections;
using UnityEngine;

public class CFocusState : CBaseState {
    public CFocusState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) { }
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
        Ctx.StopCoroutine("FocusTarget");
    }
    public override void CheckSwitchStates() {
        //Switch logic
                
        if (Ctx.IsTalking) {
            SwitchState(StateHandler.Talk());
        } 
        else if (Ctx.IsMoving) {
            SwitchState(StateHandler.Move());
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
        Ctx.IsMoving = false;
        Ctx.IsIdle = false;
        Ctx.IsUnstucking = false;
    }    
}
