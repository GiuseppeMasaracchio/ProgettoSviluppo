using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : BaseState {
    public GroundedState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base (currentContext, stateHandler, animHandler){
        IsRootState = true; //SOLO SU GROUNDED, AIRBORNE E DEAD (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic

        Ctx.MoveSpeed = 2200f;
        Ctx.JumpCount = 2;
    }
    public override void UpdateState() {
        //Update logic
        
        if (!Ctx.IsWalking) {            
            Ctx.IsIdle = true;
        }
        else Ctx.IsIdle = false;
        
        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        
    }
    public override void CheckSwitchStates() {
        //Switch logic
        if (Ctx.IsAirborne) {
            SwitchState(StateHandler.Airborne());
        }
        else if (Ctx.IsDead) {
            SwitchState(StateHandler.Dead());
        }
    }    
}
