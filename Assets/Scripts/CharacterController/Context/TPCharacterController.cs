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
    
    //Custom Components
    StateHandler _stateHandler;
    AnimHandler _animHandler;
    DevToolUI _devUI = null;

    //Player references
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _asset;
    [SerializeField] GameObject _cam;
    [SerializeField] GameObject _forward;
    Animator _animator;
    Rigidbody _playerRb;
    SphereCollider _attackCollider;

    //Root States
    private bool isDead = false;
    private bool isGrounded = false;

    //Sub States
    private bool isIdle = false;
    private bool isDamaged = false;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool isDashing = false;
    private bool isAttacking = false;
    private bool isFalling = false;

    //Context vars
    private Vector3 surfaceNormal;
    private bool onSlope;
    private bool canDMG = true;
    private bool canDash = true;
    private bool canAttack = true;
    private bool canJump = true;
    private int dashCount = 1;
    private int attackCount = 1; //Per eventuale sistema di combo
    private int jumpCount = 2;
    private float moveSpeed = 1760f;    
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

    [SerializeField]
    [Range(0f, 35f)] float slopeAngle;

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
    public bool AttackInput { get { return attackInput; } set { attackInput = value; } }
    public bool DashInput { get { return dashInput; } set { dashInput = value; } }

    public Vector3 SurfaceNormal { get { return surfaceNormal; } }
    public bool OnSlope { get { return onSlope; } }
    public int DashCount { get { return dashCount; } set { dashCount = value; } }
    public int JumpCount { get { return jumpCount; } set { jumpCount = value; } }
    public int AttackCount { get { return attackCount; } set { attackCount = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public bool CanDash { get { return canDash; } set { canDash = value; } }
    public bool CanAttack { get { return canAttack; } set { canAttack = value; } }
    public bool CanJump { get { return canJump; } set { canJump = value; } }
    public float JumpHeight { get { return jumpHeight; } set { jumpHeight = value; } }

    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public int PowerUps { get { return powerUps; } set { powerUps = value; } }
    public int Score { get { return score; } set { score = value; } }

    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool IsIdle { get { return isIdle; } set { isIdle = value; } }
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsDamaged { get { return isDamaged; } set { isDamaged = value; } }
    public bool IsWalking { get { return isWalking; } set { isWalking = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    public bool IsFalling { get { return isFalling; } set { isFalling = value; } }

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

        _animator = _asset.GetComponentInChildren<Animator>();
        _playerRb = _player.GetComponent<Rigidbody>();
        //_attackCollider = _player.GetComponentInChildren<SphereCollider>();

        _animHandler = GameObject.Find("Asset").AddComponent<AnimHandler>();
        _stateHandler = new StateHandler(this, _animHandler);

        _currentRootState = StateHandler.Airborne();
        _currentRootState.EnterState();        

        _currentSubState = StateHandler.Fall();
        _currentSubState.EnterState();

        InitializeUI();

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
        //Slow-mo
        //Time.timeScale = Time.timeScale * 0.1f;
    }

    // Update is called once per frame
    void Update() {
        UpdateCamera(_cam, _player, _forward, camInput, _currentSens);
        
        if (_devUI != null) {
            _devUI.UpdateText(this);
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
    public void OnAttack(InputValue input) {        
        if (input.Get() != null) {
            SetUpAttack();
        } else {
            attackInput = false;
        }
    }
    public void OnDash(InputValue input) {
        if (input.Get() != null) {
            SetUpDash();
        } else {
            dashInput = false;
        }
    }

    //Collision Callbacks
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            SetDMGState();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Enemy") {
            SetDMGState();
        }
    }
    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "Slope") {
            float angle = Vector3.Angle(PlayerRb.transform.up, collision.GetContact(0).normal);
            SetSlope(angle, collision.GetContact(0).normal);
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.collider.tag == "Slope") {
            onSlope = false;
            //Debug.Log("Bye from slope");
        }
    }
    
    //SetUp Methods
    private void SetUpJump() {
        if (!jumpInput) {
            jumpInput = true;
            SetJumpState();
        } else {
            return;
        }
    }
    private void SetUpDash() {
        if (!dashInput) {
            dashInput = true;
            SetDashState();
        }
        else {
            return;
        }
    }
    private void SetUpAttack() {
        if (!attackInput) {
            attackInput = true;
            SetAttackState();
        }
        else {
            return;
        }
    }
    private void SetJumpState() {
        if (jumpCount <= 0) { return; }

        if (!canJump) { return; }
        
        if (!isDamaged) {
            isJumping = true;
        }
    }
    private void SetDashState() {
        if (!canDash) { return; }         
        
        if (!isDamaged) { 
            isDashing = true;
            canDash = false;
        }
    }
    private void SetAttackState() {
        if (attackCount <= 0) { return; }
        
        if (!isDamaged) {
            canDMG = false;
            isAttacking = true;
        }
    }   
    private void SetSlope(float angle, Vector3 _surfaceNormal) {
        if (angle <= slopeAngle) {
            onSlope = true;
            surfaceNormal = _surfaceNormal;
        }
    }
    public void SetDMGState() {
        if (!canDMG) { return; }

        if (!isDashing) { 
            isDamaged = true;
        }
    }

    //Camera Methods
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

    //UI Init
    private void InitializeUI() {
        GameObject devUI = GameObject.Find("DevToolUI");

        if (devUI != null) {
            _devUI = devUI.GetComponent<DevToolUI>();
        }
    }

    //Coroutine
    public IEnumerator InitializeMoveSpeed() {
        while (moveSpeed > 600f) {
            moveSpeed = moveSpeed - ((moveSpeed * .6f) * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    public IEnumerator ResetAttack() {
        canAttack = false;
        yield return new WaitForSeconds(.2f);

        isAttacking = false;
        canDMG = true;

        yield return new WaitForSeconds(.1f);

        canAttack = true;
        if (isGrounded) {
            attackCount = 1;
        }
        yield break;
    }
    public IEnumerator ResetDash() {
        yield return new WaitForSeconds(.3f);

        isDashing = false;

        if (isGrounded) {
            yield return new WaitForSeconds(.6f);
            canDash = true;
            yield break;
        }

        yield return new WaitForSeconds(2f);
        canDash = true;

        yield break;
    }
    public IEnumerator ResetJump() {
        canJump = true;

        yield return new WaitForSeconds(.2f);
        isJumping = false;

        yield break;
    }
    public IEnumerator ResetDMG() {
        canDMG = false;
        yield return new WaitForSeconds(.2f);
        isDamaged = false;
        canDMG = true;
        yield break;
    }
    //
}
