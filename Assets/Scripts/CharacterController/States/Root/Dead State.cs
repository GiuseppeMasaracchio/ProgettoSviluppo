using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState, IContextInit {
    public DeadState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) : base (currentContext, stateHandler, animHandler){
        IsRootState = true; //SOLO SU GROUNDED, AIRBORNE E DEAD (ROOT STATES)
    }
    public override void EnterState() {
        //Enter logic
        Debug.Log("Lmao u ded");

        Ctx.AnimHandler.SetAlt(true);
        Ctx.AnimHandler.PlayDirect(AnimHandler.Dead());
        GravityOff();
        InitializeContext();
        Ctx.StartCoroutine("ReloadScene");
    }
    public override void UpdateState() {
        //Update logic

        CheckSwitchStates(); //MUST BE LAST INSTRUCTION
    }
    public override void ExitState() {
        //Exit logic
        Ctx.AnimHandler.SetAlt(false);

    }
    public override void CheckSwitchStates() {
        //Switch logic
        
    }
    public void InitializeContext() {
        //
    }
}