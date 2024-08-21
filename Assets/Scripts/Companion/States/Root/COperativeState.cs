using System.Collections;
using UnityEngine;

public class COperativeState : CBaseState {
    public COperativeState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) {
        IsRootState = true; //SOLO SU STUCK E OPERATIVE (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic
        InitializeContext();
    }
    public override void UpdateState() {
        //Update logic        

        Ctx.StorePlayerDistance();
        
        if (!Ctx.IsFocusing && !Ctx.IsTalking && Ctx.IsMoving) {
            Ctx.IsMoving = true;
            Ctx.IsIdle = false;
        }
        else if (!Ctx.IsFocusing && !Ctx.IsTalking && !Ctx.IsMoving) {
            Ctx.IsMoving = false;
            Ctx.IsIdle = true;
        }        

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
    }
    public override void CheckSwitchStates() {
        //Switch logic
        
        if (Ctx.IsStuck) {
            SwitchState(StateHandler.Stuck());
        }        
        
    }
    public void InitializeContext() {
        Ctx.IsStuck = false;
    }
    
}
