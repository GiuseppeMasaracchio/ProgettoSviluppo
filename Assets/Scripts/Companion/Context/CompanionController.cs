using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class CompanionController : MonoBehaviour {
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] VisualEffect _spark;
    [SerializeField] VisualEffect _trail;
    [SerializeField] Collider _stuckCollider;

    [SerializeField]
    [Range(0f, 5f)] float horizontalOffset;

    [SerializeField]
    [Range(0f, 5f)] float verticalOffset;

    [SerializeField]
    [Range(0f, 10f)] float maxVelocity;

    [SerializeField]
    [Range(0f, 10f)] float limitDistance;

    public string debugState;

    private CBaseState _currentRootState;
    private CBaseState _currentSubState;
    private CompanionStateHandler _stateHandler;

    private GameObject _playerHead;

    private bool isStuck = false;
    private bool isOperative = false;
    private bool isIdle = false;
    private bool isMoving = false;
    private bool isTalking = false;
    private bool isFocusing = false;
    private bool isUnstucking = false;

    private int rrToken = 0;
    private float limitDistanceMax;
    private Vector3[] targetPosition = new Vector3[4];
    private Vector3 lockedPosition;

    private bool targetLocked = false;

    private float velocity;
    private float playerDistance;

    public VisualEffect Spark { get { return _spark; } set { _spark = value; } }
    public VisualEffect Trail { get { return _trail; } set { _trail = value; } }

    public CBaseState CurrentRootState { get { return _currentRootState; } set { _currentRootState = value; } }
    public CBaseState CurrentSubState { get { return _currentSubState; } set { _currentSubState = value; } }
    public CompanionStateHandler StateHandler { get { return _stateHandler; } set { _stateHandler = value; } }

    public GameObject PlayerHead { get { return _playerHead; } set { _playerHead = value; } }
    public Collider StuckCollider { get { return _stuckCollider; } set { _stuckCollider = value; } }

    public bool IsStuck { get { return isStuck; } set { isStuck = value; } }
    public bool IsOperative { get { return isOperative; } set { isOperative = value; } }
    public bool IsIdle { get { return isIdle; } set { isIdle = value; } }
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsTalking { get { return isTalking; } set { isTalking = value; } }
    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }
    public bool IsUnstucking { get { return isUnstucking; } set { isUnstucking = value; } }

    public int RRToken { get { return rrToken; } }
    public float HorizontalOffset { get { return horizontalOffset; } }
    public float VerticalOffset { get { return verticalOffset; } }
    public float MaxVelocity { get { return maxVelocity; } }
    public float LimitDistance { get { return limitDistance; } }
    public float LimitDistanceMax { get { return limitDistanceMax; } set { limitDistanceMax = value; } }
    public Vector3[] TargetPositions { get { return targetPosition; } set { targetPosition = value; } }
    public bool TargetLocked { get { return targetLocked; } set { targetLocked = value; } }
    public Vector3 LockedPositions { get { return lockedPosition; } set { lockedPosition = value; } }
    public float Velocity { get { return velocity; } set { velocity = value; } }
    public float PlayerDistance { get { return playerDistance; } set { playerDistance = value; } }

    void Awake() {
        _stateHandler = new CompanionStateHandler(this);
        _playerHead = GameObject.Find("PlayerHead");

        limitDistanceMax = limitDistance + horizontalOffset;
        
        _currentRootState = StateHandler.Operative();
        _currentRootState.EnterState();

        _currentSubState = StateHandler.Idle();
        _currentSubState.EnterState();

    }

    // Update is called once per frame
    void Update() {
        EvaluateHealth();

        _currentRootState.UpdateState();
        _currentSubState.UpdateState();

        debugState = _currentSubState.ToString();
        if (_playerInfo.PlayerSubState == "JumpState") {
            isFocusing = true;
        }
    }
    
    private void CycleRRToken() {
        if (rrToken < 3) {
            rrToken++;
        } else {
            rrToken = 0;
        }
    }

    private void EvaluateHealth() {
        switch (_playerInfo.CurrentHp) {
            case 1: {
                    _spark.SetInt("HealthDisplayRange", (int)PlayerHealthRange.LOW);
                    break;
                }
            case 2: {
                    _spark.SetInt("HealthDisplayRange", (int)PlayerHealthRange.MID);
                    break;
                }
            case 3: {
                    _spark.SetInt("HealthDisplayRange", (int)PlayerHealthRange.HIGH);
                    break;
                }
            default: {
                    break;
                }
        }
    }

    public void LookAround() {
        StartCoroutine("LookAroundRoutine");
    }

    public void StopLookAround() {
        StopCoroutine("LookAroundRoutine");
    }

    public IEnumerator LookAroundRoutine() {
        yield return new WaitForSeconds(3f);

        Quaternion startAngle = this.transform.rotation;
        Quaternion targetAngle = new Quaternion(0f, 0f, 0f, 0f);       
        
        targetAngle.Set(startAngle.x, startAngle.y, startAngle.z, startAngle.w);
        targetAngle = Quaternion.Euler(0f, 40f, 0f);

        while (Mathf.Abs(targetAngle.eulerAngles.y - transform.rotation.eulerAngles.y) > 10f) {
            this.transform.Rotate(new Vector3(0f, 80f * Time.deltaTime, 0f)); 
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        
        targetAngle.Set(startAngle.x, startAngle.y, startAngle.z, startAngle.w);
        targetAngle = Quaternion.Euler(0f, -40f, 0f);

        while (Mathf.Abs(targetAngle.eulerAngles.y - transform.rotation.eulerAngles.y) > 10f) {
            this.transform.Rotate(new Vector3(0f, -160f * Time.deltaTime, 0f));
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (Mathf.Abs(startAngle.eulerAngles.y - transform.rotation.eulerAngles.y) > 10f) {
            this.transform.Rotate(new Vector3(0f, 80f * Time.deltaTime, 0f));
            yield return null;
        }

        yield break;
    }
}
