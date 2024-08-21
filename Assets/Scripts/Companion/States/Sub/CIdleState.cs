using System.Collections;
using UnityEngine;

public class CIdleState : CBaseState {
    public CIdleState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) { }
    public override void EnterState() {
        //Enter logic
        InitializeContext();

        Ctx.InvokeRepeating("VisionEnterIdleBehaviour", 0f, 5f);

        //Ctx.InvokeRepeating("UpdateRRToken", 0f, 2f);

        //Ctx.VisionLockedPoint = Ctx.FocusDefaultPoint.position;
        //Ctx.StartCoroutine("VisionDiscoveryRoutine");        
    }
    public override void UpdateState() {
        //Update logic        
        
        //Ctx.VisionLockedPoint = Ctx.FocusDefaultPoint.position;

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic

        Ctx.CancelInvoke("VisionEnterIdleBehaviour");
        Ctx.VisionExitIdleBehaviour();

        //Ctx.StopCoroutine("FocusTarget");
        //Ctx.StopLookAround();
    }
    public override void CheckSwitchStates() {
        //Switch logic
        
        if (Ctx.IsMoving) {
            SwitchState(StateHandler.Move());
        } 
        else if (Ctx.IsFocusing) {
            SwitchState(StateHandler.Focus());
        }
        else if (Ctx.IsTalking) {
            SwitchState(StateHandler.Talk());
        }
        else if (Ctx.IsStuck && Ctx.IsUnstucking) {
            SwitchState(StateHandler.Unstuck());
        }
    }
    public void InitializeContext() {
        Ctx.IsMoving = false;
        Ctx.IsFocusing = false;
        Ctx.IsTalking = false;
        Ctx.IsUnstucking = false;
    }    
}
