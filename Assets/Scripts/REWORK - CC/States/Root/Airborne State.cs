using UnityEngine;

public class AirborneState : BaseState, IPhysics {
    public AirborneState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        IsRootState = true; //SOLO SU GROUNDED, AIRBORNE E DEAD (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic

        Ctx.MoveSpeed = 2000f;

        if (Ctx.IsFalling) {
            InitializeJumpCount();
        }
    }
    public override void UpdateState() {
        //Update logic
        
        if (!Ctx.IsJumping) {
            Ctx.IsFalling = true;
        }
        HandleGravity(Ctx.PlayerRb);
        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        Ctx.Gravity = 9.81f;
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
    public void HandleGravity(Rigidbody rb) {
        //Gravity logic
        if (Ctx.Gravity < (9.81f * Ctx.GravityMultiplier)) {
            Ctx.Gravity = Ctx.Gravity + Ctx.GravitySpeed;
        }
        rb.AddForce(Vector3.up * -Ctx.Gravity * Time.deltaTime, ForceMode.VelocityChange);
    }
}
