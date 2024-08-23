public class CUnstuckState : CBaseState {
    public CUnstuckState(CompanionController currentContext, CompanionStateHandler stateHandler) : base(currentContext, stateHandler) { }
    public override void EnterState() {
        //Enter logic

        Ctx.InvokeRepeating("UnstuckEnterBehaviour", 0f, 3f);
        //Ctx.UnstuckEnterBehaviour(); 

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
        
        if (Ctx.IsOperative && Ctx.IsIdle) {
            SwitchState(StateHandler.Idle());
        }        
    }
    public void InitializeContext() {
        Ctx.IsMoving = false;
        Ctx.IsIdle = false;
        Ctx.IsTalking = false;
    }    
}
