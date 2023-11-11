using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState, IContextInit {
    public AttackState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic
        InitializeContext();

        GravityOff();

        Ctx.AnimHandler.Play(AnimHandler.Attack1());

        Ctx.StartCoroutine("ResetAttack");
    }
    public override void UpdateState() {
        //Ctx.PlayerRb.velocity.Set(0f, 0f, 0f);
        CheckSwitchStates();
    }
    public override void ExitState() {
        //Exit logic

        GravityOn();
        
    }
    public override void CheckSwitchStates() {
        //Switch logic
        
        if (Ctx.IsGrounded && Ctx.CanAttack && Ctx.IsWalking) {
            SwitchState(StateHandler.Walk());
        }
        else if (Ctx.CanAttack && Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }
        else if (Ctx.IsDamaged) {
            SwitchState(StateHandler.Damage());
        } 
        else if (Ctx.IsDashing) {
            SwitchState(StateHandler.Dash());
        }
        else if (Ctx.IsJumping) {
            SwitchState(StateHandler.Jump());
        }
        else if (Ctx.CanAttack && Ctx.IsFalling) {
            SwitchState(StateHandler.Fall());
        }
    }
    public void InitializeContext() {
        //Ctx.AttackInput = false;
        
        
        if (!Ctx.IsGrounded) {
            Ctx.Gravity = 0f;
        }
        

        Ctx.PlayerRb.velocity.Set(0f, 0f, 0f);
        Ctx.AttackCount--;

        Ctx.IsDashing = false;
        Ctx.IsJumping = false;
    }
    public void HandleAttack() {
        //
    }

}
