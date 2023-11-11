using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState, IContextInit {
    public DeadState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base (currentContext, stateHandler, animHandler){
        IsRootState = true; //SOLO SU GROUNDED, AIRBORNE E DEAD (ROOT STATES)
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
        
    }
    public void InitializeContext() {
        //
    }
}
