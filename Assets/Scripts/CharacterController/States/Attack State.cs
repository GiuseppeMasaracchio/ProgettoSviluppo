using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AttackState : BaseState, IContextInit {
    public AttackState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic
        InitializeContext();
        HandleAttack();
        GravityOff();

        InitializeParticle5();

        Ctx.AnimHandler.SetAlt(true);
        Ctx.AnimHandler.PlayDirect(AnimHandler.Attack1());
        Ctx.StartCoroutine("ResetAttack");
    }
    public override void UpdateState() {
        Ctx.PlayerRb.velocity.Set(0f, 0f, 0f);
        CheckSwitchStates();
    }
    public override void ExitState() {
        //Exit logic
        Ctx.AnimHandler.SetAlt(false);
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
        Ctx.AttackInput = false;

        if (!Ctx.IsGrounded) {
            Ctx.Gravity = 0f;
        }        

        Ctx.PlayerRb.velocity.Set(0f, 0f, 0f);
        Ctx.AttackCount--;

        Ctx.IsWalking = false;
        Ctx.IsIdle = false;
        Ctx.IsDashing = false;
        Ctx.IsJumping = false;
    }
    public void InitializeParticle5() {
        Ctx.Vfx.GetComponent<VisualEffect>().Stop();
        //Ctx.Vfx5.GetComponent<VisualEffect>().Reinit();
        Ctx.Vfx5.GetComponent<VisualEffect>().Play();
    }
    public void HandleAttack() {
        if (!Ctx.IsGrounded) {
            //Ctx.PlayerRb.AddForce(Vector3.up, ForceMode.VelocityChange);
        }
    }

}
