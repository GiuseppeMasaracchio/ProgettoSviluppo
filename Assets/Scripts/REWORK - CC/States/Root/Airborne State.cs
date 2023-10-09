using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AirborneState : BaseState {
    public AirborneState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        IsRootState = true; //SOLO SU GROUNDED, AIRBORNE E DEAD (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic

        if (Ctx.IsFalling) {
            InitializeJumpCount();
        }
    }
    public override void UpdateState() {
        //Update logic

        if (Ctx.MoveSpeed > 2000) {            
            Ctx.MoveSpeed = Mathf.Lerp(Ctx.MoveSpeed, 2000, 1f);
        }

        if (!Ctx.IsJumping) {
            Ctx.IsFalling = true;
        }

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        
    }
    public override void CheckSwitchStates() {
        //Switch logic

        if (Ctx.IsGrounded) {
            SwitchState(StateHandler.Grounded());
        }
        else if (Ctx.IsDead) {
            SwitchState(StateHandler.Dead());
        }
    }
    public void InitializeJumpCount() {
        if (Ctx.JumpCount > 0 && Ctx.JumpCount < 2) {
            Ctx.JumpCount--;
        }
    }
}
