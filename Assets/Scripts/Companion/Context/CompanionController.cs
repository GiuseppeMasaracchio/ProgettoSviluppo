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
    private Transform visionDiscoveryTransform;
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

    public Transform VisionDiscoveryTransform { get { return visionDiscoveryTransform; } set { visionDiscoveryTransform = value; } }
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

    }

    public void StorePlayerDistance() {
        PlayerDistance = Mathf.Abs(Vector3.Distance(this.transform.position, _playerHead.position));
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
  
    public void UpdateRRToken() {
        StartCoroutine("CycleRRToken");
    }   

    public void VisionEnterIdleBehaviour() {
        VisionLocked = false;
        StopCoroutine("VisionDiscoveryRoutine");
        StartCoroutine("VisionDiscoveryRoutine");
    }

    public void VisionExitIdleBehaviour() {
        VisionLocked = false;
        StopCoroutine("VisionDiscoveryRoutine");
    }

    public void VisionEnterTalkBehaviour() {
        VisionLocked = true;
        visionDiscoveryTransform = PlayerHead;
        StartCoroutine("VisionFocusRoutine");
    }

    public void VisionExitTalkBehaviour() {
        VisionLocked = false;
    }

    public void VisionEnterMoveBehaviour() {
        VisionLocked = true;
        visionDiscoveryTransform = FocusDefaultPoint;
        StartCoroutine("VisionFocusRoutine");
    }

    public void VisionExitMoveBehaviour() {
        VisionLocked = false;
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

    public IEnumerator VisionDiscoveryRoutine() {
        Collider[] targets = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Enemy"));

        yield return null;

        if (PlayerDistance < limitDistance) {
            VisionDiscoveryTransform = PlayerHead;
            
        } else {

            if (targets != null) {
                float distanceBuffer = 10f;

                foreach (Collider target in targets) {
                    Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out RaycastHit hitInfo, 10f);
                    
                    if (hitInfo.collider.transform == target.transform) {                         
                        float targetDistance = Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));

                        if (targetDistance < distanceBuffer) {
                            distanceBuffer = targetDistance;
                            VisionDiscoveryTransform = target.transform;
                            Debug.Log(target.transform.name + ": Locked at Distance (" + distanceBuffer + ")");
                        }
                        
                    } else {
                        VisionDiscoveryTransform = FocusDefaultPoint;

                    }    

                    yield return null;
                }                

            } else {
                VisionDiscoveryTransform = FocusDefaultPoint;                
            }
        }
        
        VisionLocked = true;
        StartCoroutine("VisionFocusRoutine");

        yield break;
    }

    public IEnumerator VisionFocusRoutine() {
        Vector3 lerpPoint = _focusPoint.position;        
        
        _focusPivot.LookAt(VisionDiscoveryTransform.position);
        VisionFocusPoint = _focusPoint.position;

        while (Mathf.Abs(Vector3.Distance(lerpPoint, VisionFocusPoint)) > 0.2f) {            
            _focusPivot.LookAt(VisionDiscoveryTransform.position);
            VisionFocusPoint = _focusPoint.position;
            _asset.LookAt(lerpPoint);            
            lerpPoint = Vector3.LerpUnclamped(lerpPoint, VisionFocusPoint, 10f * Time.deltaTime);
            yield return null;
        }

        _focusPivot.LookAt(VisionDiscoveryTransform.position);
        _asset.LookAt(VisionDiscoveryTransform.position);

        while (VisionLocked) {
            _focusPivot.LookAt(VisionDiscoveryTransform.position);
            _asset.LookAt(VisionDiscoveryTransform.position);
            yield return null;
        }

        yield break;
    }
}
