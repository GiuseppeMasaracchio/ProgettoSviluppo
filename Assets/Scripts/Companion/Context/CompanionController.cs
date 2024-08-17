using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

public class CompanionController : MonoBehaviour {
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] VisualEffect _spark;
    [SerializeField] VisualEffect _trail;
    [SerializeField] Collider _stuckCollider;

    [SerializeField] Transform _asset;
    [SerializeField] Transform _playerHead;
    [SerializeField] Transform _focusDefaultPoint;
    [SerializeField] Transform _focusPivot;
    [SerializeField] Transform _focusPoint;

    [SerializeField]
    [Range(0f, 5f)] float horizontalOffset;

    [SerializeField]
    [Range(0f, 5f)] float verticalOffset;

    [SerializeField]
    [Range(0f, 10f)] float maxVelocity;

    [SerializeField]
    [Range(0f, 10f)] float limitDistance;

    private CBaseState _currentRootState;
    private CBaseState _currentSubState;
    private CompanionStateHandler _stateHandler;    
    
    //Root States
    private bool isStuck = false;
    private bool isOperative = false;

    //Sub States
    private bool isIdle = false;
    private bool isMoving = false;
    private bool isTalking = false;
    private bool isFocusing = false;
    private bool isUnstucking = false;

    /*
    private bool canFocus = true;
    private bool canTalk = true;
    private bool canMove = true;
    */

    //VISION MODULE
    private Vector3 visionFocusPoint;
    private Vector3 visionLockedPoint;
    private bool visionLocked;

    //TRAVEL MODULE
    private Vector3[] travelDestinations = new Vector3[8];
    private Vector3 travelPosition;
    private bool travelLocked = false;

    private float velocity;
    private int rrToken = 0;
    private float limitDistanceMax;
    private float playerDistance;

    public VisualEffect Spark { get { return _spark; } set { _spark = value; } }
    public VisualEffect Trail { get { return _trail; } set { _trail = value; } }

    public CBaseState CurrentRootState { get { return _currentRootState; } set { _currentRootState = value; } }
    public CBaseState CurrentSubState { get { return _currentSubState; } set { _currentSubState = value; } }
    public CompanionStateHandler StateHandler { get { return _stateHandler; } set { _stateHandler = value; } }

    public Transform PlayerHead { get { return _playerHead; } set { _playerHead = value; } }
    public Transform FocusDefaultPoint { get { return _focusDefaultPoint; } set { _focusDefaultPoint = value; } }
    public Collider StuckCollider { get { return _stuckCollider; } set { _stuckCollider = value; } }

    public bool IsStuck { get { return isStuck; } set { isStuck = value; } }
    public bool IsOperative { get { return isOperative; } set { isOperative = value; } }
    public bool IsIdle { get { return isIdle; } set { isIdle = value; } }
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsTalking { get { return isTalking; } set { isTalking = value; } }
    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }
    public bool IsUnstucking { get { return isUnstucking; } set { isUnstucking = value; } }

    public float HorizontalOffset { get { return horizontalOffset; } }
    public float VerticalOffset { get { return verticalOffset; } }
    public float MaxVelocity { get { return maxVelocity; } }
    public float LimitDistance { get { return limitDistance; } }

    public Vector3 VisionFocusPoint { get { return visionFocusPoint; } set { visionFocusPoint = value; } }
    public Vector3 VisionLockedPoint { get { return visionLockedPoint; } set { visionLockedPoint = value; } }
    public bool VisionLocked { get { return visionLocked; } set { visionLocked = value; } }

    public Vector3[] TravelDestinations { get { return travelDestinations; } set { travelDestinations = value; } }
    public Vector3 TravelPosition { get { return TravelPosition; } set { TravelPosition = value; } }
    public bool TravelLocked { get { return travelLocked; } set { travelLocked = value; } }

    public float Velocity { get { return velocity; } set { velocity = value; } }
    public int RRToken { get { return rrToken; } }
    public float LimitDistanceMax { get { return limitDistanceMax; } set { limitDistanceMax = value; } }
    public float PlayerDistance { get { return playerDistance; } set { playerDistance = value; } }

    void Awake() {
        _stateHandler = new CompanionStateHandler(this);      

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

        if (PlayerDistance < 2.5f) { //DEBUG
            if (!isFocusing) {
                isFocusing = true;
            }
            else { return; }
        } else {
            if (isFocusing) {
                isFocusing = false;
            }
            else { return; }
        }
    }

    public void StorePlayerDistance() {
        PlayerDistance = Vector3.Distance(this.transform.position, _playerHead.position);
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

    public void UpdateRRToken() {
        StartCoroutine("CycleRRToken");
    }   

    private IEnumerator CycleRRToken() {
        int tempToken = Random.Range(1, 9);
        
        if (rrToken != tempToken) {
            rrToken = tempToken;
            
            yield break;
        }        

        while (rrToken == tempToken) {
            tempToken = Random.Range(1, 9);

            if (rrToken != tempToken) {
                rrToken = tempToken;

                yield break;
            }
        }        
    }

    public IEnumerator FocusTarget() {
        Vector3 lerpPoint = _focusPoint.position;        
        
        _focusPivot.LookAt(VisionLockedPoint);
        VisionFocusPoint = _focusPoint.position;

        while (Mathf.Abs(Vector3.Distance(lerpPoint, VisionFocusPoint)) > 0.2f) {            
            _focusPivot.LookAt(VisionLockedPoint);
            VisionFocusPoint = _focusPoint.position;
            _asset.transform.LookAt(lerpPoint);            
            lerpPoint = Vector3.LerpUnclamped(lerpPoint, VisionFocusPoint, 10f * Time.deltaTime);
            yield return null;
        }

        _focusPivot.LookAt(VisionLockedPoint);
        _asset.transform.LookAt(VisionLockedPoint);

        float timer = Time.time + 2f;
        while (Time.time < timer) {
            _focusPivot.LookAt(VisionLockedPoint);
            _asset.transform.LookAt(VisionLockedPoint);
            yield return null;
        }

        while (IsFocusing) {
            _focusPivot.LookAt(VisionLockedPoint);
            _asset.transform.LookAt(VisionLockedPoint);
            yield return null;
        }

        yield break;
    }

    public IEnumerator LookAroundRoutine() {
        yield return new WaitForSeconds(3f);

        Quaternion startAngle = _asset.transform.rotation;
        Quaternion targetAngle = new Quaternion(0f, 0f, 0f, 0f);       
        
        targetAngle.Set(startAngle.x, startAngle.y, startAngle.z, startAngle.w);
        targetAngle = Quaternion.Euler(0f, 40f, 0f);

        while (Mathf.Abs(targetAngle.eulerAngles.y - transform.rotation.eulerAngles.y) > 10f) {
            _asset.transform.Rotate(new Vector3(0f, 80f * Time.deltaTime, 0f)); 
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        
        targetAngle.Set(startAngle.x, startAngle.y, startAngle.z, startAngle.w);
        targetAngle = Quaternion.Euler(0f, -40f, 0f);

        while (Mathf.Abs(targetAngle.eulerAngles.y - transform.rotation.eulerAngles.y) > 10f) {
            _asset.transform.Rotate(new Vector3(0f, -160f * Time.deltaTime, 0f));
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (Mathf.Abs(startAngle.eulerAngles.y - transform.rotation.eulerAngles.y) > 10f) {
            _asset.transform.Rotate(new Vector3(0f, 80f * Time.deltaTime, 0f));
            yield return null;
        }

        yield break;
    }

}
