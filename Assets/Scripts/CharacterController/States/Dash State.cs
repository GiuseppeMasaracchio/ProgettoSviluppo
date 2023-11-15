using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : BaseState, IContextInit {
    public DashState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic
        InitializeContext();

        GravityOff();

        Ctx.AnimHandler.PlayDirect(AnimHandler.Dash());

        HandleDash(Ctx.PlayerRb);

        Ctx.StartCoroutine("ResetDash");
    }
    public override void UpdateState() {
        //Update logic
        Ctx.PlayerRb.velocity.Set(0f, 0f, 0f);
        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        GravityOn();

    }
    public override void CheckSwitchStates() {
        //Switch logic

        if (!Ctx.IsDashing && Ctx.IsFalling) {
            SwitchState(StateHandler.Fall());
        } 
        else if (Ctx.IsAttacking) {
            SwitchState(StateHandler.Attack());
        } 
        else if (Ctx.IsJumping) {
            SwitchState(StateHandler.Jump());
        }       
        else if (!Ctx.IsDashing && Ctx.IsGrounded && Ctx.IsWalking) {
            SwitchState(StateHandler.Walk());
        }
        else if (!Ctx.IsDashing && Ctx.IsGrounded && Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }
    }
    public void InitializeContext() {
        Ctx.DashInput = false;

        Ctx.IsWalking = false;
        Ctx.IsIdle = false;
        Ctx.IsJumping = false;
        Ctx.IsAttacking = false;
        Ctx.IsDamaged = false;
    }
    public void HandleDash(Rigidbody rb) {        
        rb.velocity.Set(0f, 0f, 0f);
        rb.AddForce(DashDirection() * 25f, ForceMode.Impulse);
    }
    private Vector3 DashDirection() {
        if (Ctx.OnSlope) {
            Vector3 direction = Vector3.ProjectOnPlane(Ctx.Asset.transform.forward, Ctx.SurfaceNormal);
            return direction;
        }
        else return Ctx.Asset.transform.forward;
    }
}
