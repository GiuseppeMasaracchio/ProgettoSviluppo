using UnityEngine;

public class FallState : BaseState, IPhysics, IWalk {
    public FallState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base(currentContext, stateHandler, animHandler) {
        //State Constructor
    }
    public override void EnterState() {
        //Enter logic
        if (Ctx.IsAirborne) {
            Ctx.IsIdle = false;
            Ctx.AnimHandler.Play(AnimHandler.Fall());
        }
    }
    public override void UpdateState() {
        //Update logic
        if (Ctx.MoveInput != Vector2.zero) {
            Ctx.Player.transform.forward = Ctx.PlayerForward.transform.forward;
        }

        //HandleGravity(Ctx.PlayerRb);
        HandleWalk();
        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        
    }
    public override void CheckSwitchStates() {
        //Switch logic

        if (Ctx.IsWalking && Ctx.IsGrounded) {
            SwitchState(StateHandler.Walk());
        }
        else if (Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
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
    }
    public void HandleGravity(Rigidbody rb) {
        //Gravity logic
        if (Ctx.Gravity < (3.14f * Ctx.GravityMultiplier)) {
            Ctx.Gravity = Ctx.Gravity + Ctx.GravitySpeed;
        }
        rb.AddForce(Vector3.up * -Ctx.Gravity, ForceMode.Force);
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
