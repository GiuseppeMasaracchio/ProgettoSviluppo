using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : BaseState, IContextInit {
    public GroundedState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base (currentContext, stateHandler, animHandler){
        IsRootState = true; //SOLO SU GROUNDED, AIRBORNE E DEAD (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic
        InitializeContext();
    }
    public override void UpdateState() {
        //Update logic
        if (!Ctx.IsWalking && !Ctx.IsAttacking && !Ctx.IsDashing) {            
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
        if (!Ctx.IsGrounded) {
            SwitchState(StateHandler.Airborne());
        }
        else if (Ctx.IsDead) {
            SwitchState(StateHandler.Dead());
        }
    }   
    public void InitializeContext() {
        Ctx.IsFalling = false;
        Ctx.IsJumping = false;

        Ctx.StopCoroutine("InitializeMoveSpeed");

        Ctx.MoveSpeed = 1760;
        Ctx.JumpCount = 2;
        Ctx.AttackCount = 1;
    }
}
