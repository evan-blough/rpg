using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CharacterState { STANDING, WALKING, RUNNING, JUMPING, FALLING }
public class CharacterHandler : NonPlayerCharacter
{
    public OverworldPartyHandler partyHandler;
    public CharacterController controller;
    public CharacterState state;
    public PlayerControls controls;
    public Queue<Vector3> moveHistory = new Queue<Vector3>(20);
    Vector3 direction;
    Vector3 leaderPosition;
    Vector2 runMove;
    Vector2 walkMove;
    Vector2 move;

    float speedMultiplier = 1f;
    float followDistance = 1.5f;
    float jumpHeight = 3;
    float fallMultiplier = 2f;
    int directionMultiplier = 1;
    Vector3 velocity;
    public bool isGrounded;
    public float runDirection = 0f;
    public Transform groundCheck;
    public float groundDistance = 2f;
    public LayerMask groundMask;

    private void Awake()
    {
        controls = ControlsHandler.instance.playerControls;

        controls.overworld.Run.performed += ctx => runMove = ctx.ReadValue<Vector2>();
        controls.overworld.Run.canceled += ctx => runMove = Vector2.zero;

        controls.overworld.Walk.performed += ctx => walkMove = ctx.ReadValue<Vector2>();
        controls.overworld.Walk.canceled += ctx => walkMove = Vector2.zero;

        controls.overworld.Jump.performed += ctx => OnJump();
    }

    private void Start()
    {
        cam = GameManager.instance.cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        sprite.gameObject.transform.rotation = Camera.main.transform.rotation;


        moveHistory.Enqueue(transform.position);
        if (moveHistory.Count > 15) moveHistory.Dequeue();

        if (partyHandler.leadCharacter == this)
        {
            GetLeadCharacterMove();
        }
        else
        {
            int index = partyHandler.currParty.IndexOf(this);
            CharacterHandler leader = partyHandler.currParty.ElementAt(index - 1);
            FollowLeadCharacter(leader, index);
        }
        Jump();
        Move();
        StateCheck();
    }
    public void OnJump()
    {
        if (isGrounded)
        {
            state = CharacterState.JUMPING;
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    public void Jump()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        else if (velocity.y > 0 && state == CharacterState.JUMPING)
        {
            velocity.y += Physics2D.gravity.y * (fallMultiplier - 1.5f) * Time.deltaTime;
        }

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

        //gravity
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

    public void FollowLeadCharacter(CharacterHandler leader, int charIndex)
    {
        if (leader.moveHistory.Count > 5)
        {
            leaderPosition = leader.moveHistory.Dequeue();

            // need position relative to main camera in world space to adjust for rotation
            leaderPosition = Camera.main.transform.InverseTransformPoint(leaderPosition);
            Vector3 posRelToCam = Camera.main.transform.InverseTransformPoint(transform.position);


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
        //walk
        float horizontal = move.x;
        float vertical = move.y;

        // make movement relative to the camera
        var forward = cam.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        var right = cam.transform.right;
        right.y = 0f;
        right.Normalize();

        direction = forward * move.y + right * move.x;
        directionMultiplier = horizontal < 0 ? -1 : horizontal == 0 ? directionMultiplier : 1;


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            currentSpeed = runMove != Vector2.zero ? speed * speedMultiplier : speed * speedMultiplier / 2.0f;
            animator.SetFloat("Speed", currentSpeed / 11);

            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
            runDirection = directionMultiplier * Vector3.Angle(moveDir, Camera.main.transform.forward);
        }
        else if (state != CharacterState.JUMPING && state != CharacterState.FALLING)
        {
            animator.SetFloat("Speed", 0f);
            state = CharacterState.STANDING;
        }
    }

    public void StateCheck()
    {

        if (state != CharacterState.STANDING)
        {
            if (state == CharacterState.JUMPING)
                animator.SetBool("Jumping", true);
            else if (state == CharacterState.FALLING)
                animator.SetBool("Falling", true);

            animDirection = GetDirection(runDirection);
            animator.SetFloat("Direction", animDirection);
        }
        else
        {
            var angleDif = CameraHandler.AngleNormalization(sprite.transform.eulerAngles.y);

            animDirection = GetDirection(transform.eulerAngles.y - angleDif);
            animator.SetFloat("Direction", animDirection);
        }
        if (animDirection <= 7 && animDirection >= 5)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    public void OnEnable()
    {
        controls.overworld.Enable();
    }

    public void OnDisable()
    {
        controls.overworld.Disable();
    }
}