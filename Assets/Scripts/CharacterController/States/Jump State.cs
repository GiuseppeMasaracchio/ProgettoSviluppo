using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState, IWalk {
    public JumpState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic

        //Ctx.AttackCollider.enabled = true;
        Ctx.Gravity = 9.81f;
        Ctx.JumpCount--;
        Ctx.JumpInput = false;        
        Ctx.AnimHandler.Play(AnimHandler.Jump());

        HandleJump(Ctx.PlayerRb);
    }
    public override void UpdateState() {
        //Update logic
        if (Ctx.MoveInput != Vector2.zero) {
            Ctx.Player.transform.forward = Ctx.PlayerForward.transform.forward;
        }

        HandleWalk();
        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        //Ctx.AttackCollider.enabled = false;
    }
    public override void CheckSwitchStates() {
        //Switch logic
        
        if (Ctx.IsFalling) {
            SwitchState(StateHandler.Fall());
        }
        else if (Ctx.IsDamaged) {
            SwitchState(StateHandler.Damage());
        }
        else if (Ctx.IsJumping) {
            SwitchState(StateHandler.Jump());
        }
        else if (Ctx.IsDashing) {
            SwitchState(StateHandler.Dash());
        }
        else if (Ctx.IsAttacking) {
            SwitchState(StateHandler.Attack());
        }
    }
    private void HandleJump(Rigidbody rb) {
        //Jump Logic
        rb.velocity.Set(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * Ctx.JumpHeight, ForceMode.Impulse);
        ResetJump();
    }
    private void ResetJump() {
        Ctx.JumpInput = false;
        Ctx.IsJumping = false;
    }
    public void HandleWalk() {
        if (Direction() == Vector3.zero) { return; }
        Ctx.Asset.transform.forward = Direction();
        Ctx.PlayerRb.AddForce(Direction() * Ctx.MoveSpeed * .9f * Time.deltaTime, ForceMode.Force);
        SpeedControl();
    }
    private Vector3 Direction() {
        Vector3 direction = Ctx.Player.transform.forward * Ctx.MoveInput.y + Ctx.Player.transform.right * Ctx.MoveInput.x;
        return direction;
    }
    private void SpeedControl() {
        Vector3 flatvelocity = new Vector3(Ctx.PlayerRb.velocity.x, 0f, Ctx.PlayerRb.velocity.z);
        if (flatvelocity.magnitude > Ctx.MoveSpeed) {
            Vector3 limvelocity = flatvelocity.normalized * Ctx.MoveSpeed;
            Ctx.PlayerRb.velocity = new Vector3(limvelocity.x, Ctx.PlayerRb.velocity.y, limvelocity.z);
        }
    }
}
