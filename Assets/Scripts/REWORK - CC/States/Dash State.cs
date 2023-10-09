using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : BaseState {
    public DashState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic

    }
    public override void UpdateState() {
        //Update logic

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic

        Ctx.IsDashing = false;
    }
    public override void CheckSwitchStates() {
        //Switch logic

        if (Ctx.IsFalling) {
            SwitchState(StateHandler.Fall());
        } 
        else if (Ctx.IsAttacking) {
            SwitchState(StateHandler.Attack());
        } 
        else if (Ctx.IsJumping) {
            SwitchState(StateHandler.Jump());
        }       
        else if (Ctx.IsWalking) {
            SwitchState(StateHandler.Walk());
        }
        else if (Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }
    }    
}
