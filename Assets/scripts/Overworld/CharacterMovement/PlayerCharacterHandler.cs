using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CharacterState { STANDING, WALKING, RUNNING, JUMPING, FALLING }
public class PlayerCharacterHandler : CharacterHandler
{
    public OverworldPartyHandler partyHandler;
    public CharacterState state;
    public PlayerControls controls;
    public Queue<Vector3> moveHistory = new Queue<Vector3>(20);
    Vector3 leaderPosition;
    Vector2 runMove;
    Vector2 walkMove;
    bool jumped;
    Ray ray;

    float speedMultiplier = 1f;
    float followDistance = 1.5f;
    float jumpHeight = 3;
    float fallMultiplier = 2f;

    private void Awake()
    {
        controls = ControlsHandler.instance.playerControls;

        controls.overworld.Run.performed += ctx => runMove = ctx.ReadValue<Vector2>();
        controls.overworld.Run.canceled += ctx => runMove = Vector2.zero;

        controls.overworld.Walk.performed += ctx => walkMove = ctx.ReadValue<Vector2>();
        controls.overworld.Walk.canceled += ctx => walkMove = Vector2.zero;

        controls.overworld.Jump.performed += ctx => OnJump(partyHandler.leadCharacter == this);
    }

    private void Start()
    {
        cam = SceneManager.instance.cam.transform;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveHistory.Enqueue(transform.position);

        if (partyHandler.leadCharacter == this)
        {
            GetLeadCharacterMove();
        }
        else
        {
            int index = partyHandler.currParty.IndexOf(this);
            PlayerCharacterHandler leader = partyHandler.currParty.ElementAt(index - 1);
            FollowLeadCharacter(leader);
        }
        VerticalMovement();
        Move();
        AnimationStateCheck();
    }
    public void OnJump(bool isLeader = false)
    {
        if (isGrounded && isLeader)
        {
            state = CharacterState.JUMPING;
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    public override void VerticalMovement()
    {
        var point = new Vector3(controller.transform.position.x, controller.transform.position.y - (controller.height / 3) + .1f, controller.transform.position.z);
        //jump... added raycast check prevents hanging on to edge of ground when on cliff
        isGrounded = Physics.OverlapSphere(point, controller.radius, groundMask).Any()
            && Physics.Raycast(transform.position, Vector3.down, groundDistance * 2, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        else if (velocity.y > 0 && state == CharacterState.JUMPING)
        {
            velocity.y += Physics2D.gravity.y * (fallMultiplier - 1.5f) * Time.deltaTime;
        }

        //gravity
        if (!isGrounded)
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void GetLeadCharacterMove()
    {
        if (runMove != Vector2.zero)
        {
            if (state != CharacterState.JUMPING && state != CharacterState.FALLING)
                state = CharacterState.RUNNING;
            move = runMove;
        }
        else if (walkMove != Vector2.zero)
        {
            if (state != CharacterState.JUMPING && state != CharacterState.FALLING)
                state = CharacterState.WALKING;

            move = walkMove;
            move.x /= 2.5f;
            move.y /= 2.5f;
        }
        else
        {
            if (state != CharacterState.JUMPING && state != CharacterState.FALLING)
                state = CharacterState.STANDING;

            move = Vector2.zero;
        }
    }

    public void FollowLeadCharacter(PlayerCharacterHandler leader)
    {
        if (leader.moveHistory.Count > 5)
        {
            leaderPosition = leader.moveHistory.Dequeue();

            if (leaderPosition.y > transform.position.y + 1.5f)
            {
                OnJump(true);
            }

            // need position relative to main camera in world space to adjust for rotation
            leaderPosition = mainCamera.transform.InverseTransformPoint(leaderPosition);
            Vector3 posRelToCam = mainCamera.transform.InverseTransformPoint(transform.position);


            if (Vector3.Distance(leaderPosition, posRelToCam) < followDistance)
                leaderPosition = Vector3.zero;
            else if (Vector3.Distance(leaderPosition, posRelToCam) < followDistance + .5f)
            {
                speedMultiplier = 1f * ((Vector3.Distance(leaderPosition, posRelToCam) - followDistance) * 2f) + .1f;
            }
            else
                speedMultiplier = 1f;

            if (leaderPosition != Vector3.zero)
                leaderPosition = (leaderPosition - posRelToCam).normalized;

            move = new Vector2(leaderPosition.x, leaderPosition.z);
        }
        else
            move = new Vector2(0f, 0f);


        if (state != CharacterState.JUMPING && state != CharacterState.FALLING)
        {
            if (move.magnitude > .55f)
            {
                state = CharacterState.RUNNING;
            }
            else if (move.magnitude > .1f && move.magnitude <= .55f)
            {
                move /= 2.5f;
                state = CharacterState.WALKING;
            }
            else
                state = CharacterState.STANDING;
        }
    }

    public override void Move()
    {
        ApplyMoveVector();
        currentSpeed = runMove != Vector2.zero ? speed * speedMultiplier : speed * speedMultiplier / 2.0f;

        base.Move();
    }

    public override void AnimationStateCheck()
    {
        if (isGrounded && (state == CharacterState.JUMPING || state == CharacterState.FALLING))
        {
            state = CharacterState.STANDING;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
        else if (!isGrounded && velocity.y > 0)
        {
            state = CharacterState.JUMPING;
        }
        else if (!isGrounded && state != CharacterState.JUMPING && velocity.y < 0)
        {
            state = CharacterState.FALLING;
        }

        if (direction.magnitude < 0.1f && state != CharacterState.JUMPING && state != CharacterState.FALLING)
        {
            animator.SetFloat("Speed", 0f);
            state = CharacterState.STANDING;
        }

        if (state != CharacterState.STANDING)
        {
            if (state == CharacterState.JUMPING)
                animator.SetBool("Jumping", true);
            else if (state == CharacterState.FALLING)
                animator.SetBool("Falling", true);
        }
        base.AnimationStateCheck();
    }

    public void OnEnable()
    {
        controls.overworld.Enable();
    }

    public void OnDisable()
    {
        moveHistory.Clear();
        currentSpeed = 0f;
        move = Vector2.zero;
        leaderPosition = Vector2.zero;
        speedMultiplier = 1f;
        controls.overworld.Disable();
    }
}