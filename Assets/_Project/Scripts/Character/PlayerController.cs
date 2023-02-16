using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStateMachineUser
{
    [SerializeField] private CharacterParametersConfig characterParametersConfig;
    [SerializeField] private CharacterAnimStringConfig characterAnimsConfig;

    private StateMachine<PlayerController> stateMachine;
    private State<PlayerController> stateCharIdle;
    private State<PlayerController> stateCharRun;
    private State<PlayerController> stateCharJump;

    private Animator animator;
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private Vector3 velocity;
    private Vector3 rawAxis = Vector3.zero;
    private Vector2 lastRawAxis = Vector2.zero;
    private Vector3 direction;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CameraController cameraController;

    private bool isRunningJumpUpAnimation;
    private bool isRunningLandAnimation;

    private bool isCollisionBelow = false;

    Vector3 previousPos = Vector3.zero;

    public Animator Animator { get => animator; set => animator = value; }
    public CharacterAnimStringConfig CharacterAnimsConfig { get => characterAnimsConfig; set => characterAnimsConfig = value; }
    public Vector3 Velocity { get => velocity; set => velocity = value; }
    public bool IsCollisionBelow { get => isCollisionBelow; set => isCollisionBelow = value; }
    public CameraController CameraController { get => cameraController; set => cameraController = value; }
    public CharacterParametersConfig CharacterParametersConfig { get => characterParametersConfig; set => characterParametersConfig = value; }




    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        stateMachine = new StateMachine<PlayerController>(this);
        stateCharIdle = new StateCharIdle(stateMachine);
        stateCharRun = new StateCharRun(stateMachine);
        stateCharJump = new StateCharJump(stateMachine);
        
        stateMachine.StateChange(stateCharJump);
        animator.Play(characterAnimsConfig.STANDING_LOOP);
    }

    // Update is called once per frame
    private void Update()
    {
        CharacterInputMove();
        stateMachine.StateHandleInput();
        stateMachine.StateLogicUpdate();
        stateMachine.StatePhysicsUpdate();
        PhysicsHandle();
    }

    public void PhysicsHandle()
    {
        if (stateMachine.CurrentState == stateCharJump)
            velocity -= new Vector3(0f, EnviromentController.gravity, 0f);

        transform.position += velocity * Time.deltaTime;
        
        direction = transform.position - previousPos;
        direction.y = 0.0f;
        direction = direction.normalized;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        previousPos = transform.position;
    }

    public void CharacterJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isRunningJumpUpAnimation = true;
            animator.Play(CharacterAnimsConfig.JUMP_TO_TOP);
            StartCoroutine(JumpAfter(characterParametersConfig.waitTimeToJump));
        }
    }

    IEnumerator JumpAfter(float time)
    {
        yield return new WaitForSeconds(time);

        isRunningJumpUpAnimation = false;
        velocity = new Vector3(velocity.x, characterParametersConfig.jumpSpeed, velocity.z);
        stateMachine.StateChange(stateCharJump);
    }

    public void CharacterInputMove()
    {
        rawAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
    }

    public void CharacterPhysicsGroundMove()
    {
        if (direction == Vector3.zero)
        {
            direction = transform.forward;
        }

        Vector3 vecRotated = Quaternion.Euler( new Vector3( 0.0f, Camera.main.transform.rotation.eulerAngles.y, 0.0f ) ) * rawAxis;
        velocity += vecRotated * characterParametersConfig.moveUpSpeed * Time.deltaTime;

        float currentSpeed = new Vector2(velocity.x, velocity.z).magnitude;

        if (currentSpeed > characterParametersConfig.moveSpeedMax * Time.deltaTime)
        {
            Vector2 newSpeed = Vector2.ClampMagnitude(new Vector2(velocity.x, velocity.z), characterParametersConfig.moveSpeedMax * Time.deltaTime);
            velocity.x = newSpeed.x;
            velocity.z = newSpeed.y;
        }

        if (Mathf.Abs(velocity.x) > characterParametersConfig.moveDownSpeed * Time.deltaTime)
            velocity.x -= Mathf.Sign(velocity.x) * characterParametersConfig.moveDownSpeed * Time.deltaTime;
        else
            velocity.x = 0f;
        if (Mathf.Abs(velocity.z) > characterParametersConfig.moveDownSpeed * Time.deltaTime)
            velocity.z -= Mathf.Sign(velocity.z) * characterParametersConfig.moveDownSpeed * Time.deltaTime;
        else
            velocity.z = 0f;

        //Debug.Log("velocity magnitude = " + new Vector2(velocity.x, velocity.z).magnitude);
    }

    public void CharacterPhysicsJumpMove()
    {
        
    }

    public void CharacterPhysicsStopMove()
    {
        if (velocity == Vector3.zero && rawAxis == Vector3.zero)
        {
            stateMachine.StateChange(stateCharIdle);
            animator.Play(characterAnimsConfig.STANDING_LOOP);
        }
    }

    public void CharacterInputMoveStart()
    {
        if (rawAxis.magnitude > 0f && !isRunningLandAnimation)
        {
            if (velocity == Vector3.zero)
            {
                velocity.x = rawAxis.x * characterParametersConfig.moveSpeedMin * Time.deltaTime;
                velocity.z = rawAxis.y * characterParametersConfig.moveSpeedMin * Time.deltaTime;
            }
            stateMachine.StateChange(stateCharRun);
            animator.Play(characterAnimsConfig.RUNNING_LOOP);
        }
    }

    public void ChangeAnimationSpeedByRunSpeed()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == characterAnimsConfig.RUNNING_LOOP)
        {
            float value = characterParametersConfig.runAnimationSpeedMin
                + Mathf.Lerp(
                    0,
                    characterParametersConfig.runAnimationSpeedMax - characterParametersConfig.runAnimationSpeedMin,
                    (new Vector2(velocity.x, velocity.z).magnitude / Time.deltaTime - characterParametersConfig.moveSpeedMin) / (characterParametersConfig.moveSpeedMax - characterParametersConfig.moveSpeedMin)
                    );

            animator.SetFloat("Speed",value);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stateMachine.CurrentState == stateCharJump)
        {
            if (velocity.y <= 0f && !isCollisionBelow)
            {
                velocity.y = 0f;
                ChangeAnimationToLandGround();
                isCollisionBelow = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (stateMachine.CurrentState == stateCharJump)
        {
            isCollisionBelow = false;
        }
    }

    public void ChangeAnimationToLandGround()
    {
        if (new Vector2(velocity.x, velocity.z).magnitude > 0)
            stateMachine.StateChange(stateCharRun);
        else
            stateMachine.StateChange(stateCharIdle);
        animator.Play(CharacterAnimsConfig.TOP_TO_GROUND);
        isRunningLandAnimation = true;
        StartCoroutine(LandCompleteAfter(characterParametersConfig.waitTimeToLand));
    }

    private IEnumerator LandCompleteAfter(float time)
    {
        yield return new WaitForSeconds(time);

        isRunningLandAnimation = false;
        if (stateMachine.CurrentState == stateCharRun)
        {
            animator.Play(characterAnimsConfig.RUNNING_LOOP);
        }
    }

    public void OnReleaseJumpButtonInAir()
    {
        if (!Input.GetButton("Jump"))
        {
            if (velocity.y > 0f)
            {
                velocity = new Vector3(velocity.x, 0f, velocity.z);
            }
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 250, 30), "Current horizontal velocity = " + new Vector2(velocity.x, velocity.z));
    }
}
