using UnityEngine;

public abstract class BaseState {
    private bool _isRootState = false;

    private TPCharacterController _ctx;
    private StateHandler _stateHandler;
    private AnimHandler _animHandler;

    protected bool IsRootState { set { _isRootState = value; } }
    protected TPCharacterController Ctx { get { return _ctx; } }
    protected StateHandler StateHandler { get { return _stateHandler; } }
    protected AnimHandler AnimHandler { get { return _animHandler; } }

    public BaseState(TPCharacterController currentContext, StateHandler stateHandler, AnimHandler animHandler) {
        _ctx = currentContext;
        _stateHandler = stateHandler;
        _animHandler = animHandler;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    protected void SwitchState(BaseState newState) {          
        if (newState._isRootState) {
            _ctx.CurrentRootState.ExitState();
            _ctx.CurrentRootState = newState;
            _ctx.CurrentRootState.EnterState();
        } else {
            _ctx.CurrentSubState.ExitState();
            _ctx.CurrentSubState = newState;
            _ctx.CurrentSubState.EnterState();
        }
    }
}
