using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState {
    public JumpState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic

        Ctx.JumpCount--;
        Ctx.JumpInput = false;        
        Ctx.AnimHandler.Play(AnimHandler.Jump());

        HandleJump(Ctx.PlayerRb);
    }
    public override void UpdateState() {
        //Update logic

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        
    }
    public override void CheckSwitchStates() {
        //Switch logic

        if (Ctx.IsFalling) {
            SwitchState(StateHandler.Fall());
        }
        else if (Ctx.IsDamaged) {
            SwitchState(StateHandler.Damage());
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
        rb.AddForce(Vector3.up * Ctx.JumpSpeed * 9.81f, ForceMode.Force);
        ResetJump();
    }
    private void ResetJump() {
        Ctx.JumpInput = false;
        Ctx.IsJumping = false;
    }
}
