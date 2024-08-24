public class CStuckState : CBaseState {
    public CStuckState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) {
        IsRootState = true; //SOLO SU STUCK E OPERATIVE (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic
        InitializeContext();
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
        
        if (Ctx.IsOperative) {
            SwitchState(StateHandler.Operative());
        }        
        
    }
    public void InitializeContext() {
        Ctx.IsOperative = false;

        Ctx.IsMoving = false;
        Ctx.IsIdle = false;
        Ctx.IsTalking = false;
        Ctx.IsUnstucking = true;
    }    
}
