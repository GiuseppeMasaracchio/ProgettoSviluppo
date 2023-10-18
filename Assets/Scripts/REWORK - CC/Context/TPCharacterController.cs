using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

enum ActiveData {
    Slot_ID,
    Name,
    PowerUps,
    Score,
    CurrentHp,
    Runtime
}
public class TPCharacterController : MonoBehaviour
{
    //State reference
    BaseState _currentRootState;
    BaseState _currentSubState;
    StateHandler _stateHandler;
    AnimHandler _animHandler;
    DevToolUI _devUI;

    //Player references
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _asset;
    [SerializeField] GameObject _cam;
    [SerializeField] GameObject _forward;
    Rigidbody _playerRb;
    Animator _animator;
    SphereCollider _attackCollider;

    //State vars
    private bool isDead = false;
    private bool isIdle = false;
    private bool isGrounded = false;
    private bool isAirborne = false;
    private bool isDamaged = false;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool isDashing = false;
    private bool isAttacking = false;
    private bool isFalling = false;
    private bool isApproaching = false;

    //Context vars
    private int jumpCount = 2;
    private float moveSpeed;
    private float camycurrent;
    private float camytarget;

    private object[] activeData;
    private int currentHp;
    private int powerUps;
    private int score;

    private float xaxis;
    private float yaxis;
    private float _currentSens;

    private float gravity = 9.81f;

    [SerializeField]
    [Range(1f, 100f)] float gravityMultiplier;

    [SerializeField]
    [Range(0.1f, 10f)] float gravitySpeed;

    [SerializeField]
    [Range(0.1f, 80f)] float jumpHeight;

    [SerializeField]
    [Range(0.1f, 800f)] float _sens;
    
    [SerializeField]
    [Range(0.01f, 5f)] float _sensRatio;

    //Input vars
    private Vector2 camInput;
    private Vector2 moveInput;
    private bool jumpInput;
    private bool attackInput;
    private bool dashInput;

    //Constructors
    public float CurrentSens { get { return _currentSens; } }
    public float Gravity { get { return gravity; } set { gravity = value; } }
    public float GravityMultiplier { get { return gravityMultiplier; } }
    public float GravitySpeed { get { return gravitySpeed; } }

    public Vector2 CamInput { get { return camInput; } }
    public Vector2 MoveInput { get { return moveInput; } }
    public bool JumpInput { get { return jumpInput; } set { jumpInput = value; } }
    public bool AttackInput { get { return attackInput; } }
    public bool DashInput { get { return dashInput; } }

    public int JumpCount { get { return jumpCount; } set { jumpCount = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float JumpHeight { get { return jumpHeight; } set { jumpHeight = value; } }

    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public int PowerUps { get { return powerUps; } set { powerUps = value; } }
    public int Score { get { return score; } set { score = value; } }

    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool IsIdle { get { return isIdle; } set { isIdle = value; } }
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsAirborne { get { return isAirborne; } set { isAirborne = value; } }
    public bool IsDamaged { get { return isDamaged; } set { isDamaged = value; } }
    public bool IsWalking { get { return isWalking; } set { isWalking = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    public bool IsFalling { get { return isFalling; } set { isFalling = value; } }
    public bool IsApproaching { get { return isApproaching; } set { isApproaching = value; } }

    public GameObject Player { get { return _player; } }
    public GameObject Asset { get { return _asset; } }
    public GameObject Camera { get { return _cam; } }
    public GameObject PlayerForward { get { return _forward; } }
    public Rigidbody PlayerRb { get { return _playerRb; } }
    public Animator Animator { get { return _animator; } }
    public SphereCollider AttackCollider { get { return _attackCollider; } set { _attackCollider = value; } }

    public BaseState CurrentRootState { get { return _currentRootState; } set { _currentRootState = value; } }
    public BaseState CurrentSubState { get { return _currentSubState; } set { _currentSubState = value; } }
    public StateHandler StateHandler { get { return _stateHandler; } set { _stateHandler = value; } }
    public AnimHandler AnimHandler { get { return _animHandler; } set { _animHandler = value; } }

    // Awake is called before the Start 
    void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;

        _animator = GameObject.Find("PlayerAsset").GetComponent<Animator>();
        _playerRb = _player.GetComponent<Rigidbody>();
        _attackCollider = _player.GetComponentInChildren<SphereCollider>();

        _animHandler = _player.AddComponent<AnimHandler>();
        _stateHandler = new StateHandler(this, _animHandler);

        _currentRootState = StateHandler.Airborne();
        _currentRootState.EnterState();        

        _currentSubState = StateHandler.Fall();
        _currentSubState.EnterState();

        _devUI = GameObject.Find("DevToolUI").GetComponent<DevToolUI>();
        
        //activeData = DBVault.GetActiveData();
        //Debug.Log(activeData[(int)ActiveData.Name]);

        /*
        if (activeData == null) {
            DBVault.SetActiveSlot(1);
            activeData = DBVault.GetActiveData();
        }

        currentHp = (int)activeData[(int)ActiveData.CurrentHp];
        powerUps = (int)activeData[(int)ActiveData.PowerUps];
        score = (int)activeData[(int)ActiveData.Score];

        if (powerUps >= 2) {
            jumpCount = 2;
        } else {
            jumpCount = 1;
        }
        */

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        UpdateCamera(_cam, _player, _forward, camInput, _currentSens);

        _devUI.UpdateText(this);
        
        if (isGrounded) {
            isFalling = false;
            isAirborne = false;
        } else {
            isAirborne = true;
        }

        if (_playerRb.velocity.y < -5f) {
            isFalling = true;
        }
    }
    void FixedUpdate() {
        _currentRootState.UpdateState();
        _currentSubState.UpdateState();
    } 
    
    //Input Callbacks
    public void OnControlsChanged(PlayerInput input) {
        if (input.currentControlScheme == "Gamepad") {
            _currentSens = _sens;
        } else if (input.currentControlScheme == "Keyboard&Mouse") {
            _currentSens = _sens * _sensRatio;
        }
    }
    public void OnMove(InputValue input) {
        moveInput = input.Get<Vector2>();

        if (moveInput == Vector2.zero) {
            isWalking = false;
        }
        else { 
            isWalking = true;
        }
    }
    public void OnLook(InputValue input) {
        camInput = input.Get<Vector2>();
    }
    public void OnJump(InputValue input) {
        if (input.Get() != null) {
            SetUpJump();
        } else {
            jumpInput = false;
        }
    }
    public void OnFire(InputValue input) {
        if (input.Get() != null) {
            //attackInput = true; //Ricordati di settare la reset condition
            //isAttacking = true;
            //Debug.Log(isAttacking); //Ho cambiato la value type su InputActionAsset, da float ad Analog (1, null)
        } else {
            //isAttacking = false;
        }
    }
    public void OnDash(InputValue input) {
        if (input.Get() != null) {
            //dashInput = true; //Ricordati di settare la reset condition
            //isDashing = true;
            //Debug.Log(isDashing); //Ho cambiato la value type su InputActionAsset, da float ad Analog (1, null)
        } else {
            //isDashing = false;
        }
    }

    //SetUp Methods
    private void SetUpJump() {
        if (!jumpInput) {
            jumpInput = true;
            SetJumpState();
        } else {
            isJumping = false;
        }
    }
    private void SetJumpState() {
        if (jumpCount == 0) {
            return;
        } else {
            isJumping = true;
        }
    }
    private void UpdateCamera(GameObject cam, GameObject player, GameObject forward, Vector2 mouseInput, float sens) {
        VerticalSmoothCam(cam, player);

        CalculateCamMotion(mouseInput, sens);

        CamRotation(cam, forward);
    }
    private void CalculateCamMotion(Vector2 mouseInput, float sens) {
        yaxis += mouseInput.x * sens * Time.deltaTime;
        xaxis -= mouseInput.y * sens * Time.deltaTime;
        xaxis = Mathf.Clamp(xaxis, -30f, 60f);
    }
    private void CamRotation(GameObject cam, GameObject forward) {
        cam.transform.rotation = Quaternion.Euler(xaxis, yaxis, 0f);
        forward.transform.rotation = Quaternion.Euler(0f, yaxis, 0f);
    }
    private void VerticalSmoothCam(GameObject cam, GameObject player) {
        camycurrent = cam.transform.position.y;
        camytarget = player.transform.position.y;
        float camylerp = Mathf.Lerp(camycurrent, camytarget, .025f);
        if (camycurrent < camytarget) {
            cam.transform.position = new Vector3(player.transform.position.x, camylerp, player.transform.position.z);
        }
        else {
            cam.transform.position = player.transform.position;
        }
    }   
}
