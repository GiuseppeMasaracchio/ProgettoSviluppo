using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState {
    public AttackState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic
        Ctx.AnimHandler.Play(AnimHandler.Attack());
    }
    public override void UpdateState() {        
        Ctx.IsIdle = false;
        CheckSwitchStates();
    }
    public override void ExitState() {
        //Exit logic

    }
    public override void CheckSwitchStates() {
        //Switch logic

        if (Ctx.IsWalking) {
            SwitchState(StateHandler.Walk());
        }
        else if (Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }
        else if (Ctx.IsDamaged) {
            SwitchState(StateHandler.Damage());
        } 
        else if (Ctx.IsDashing) {
            SwitchState(StateHandler.Dash());
        }        
    }   
}
