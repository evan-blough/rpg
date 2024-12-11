using System.Collections;
using UnityEngine;

public class NonPlayerCharacterHandler : CharacterHandler
{
    private bool canMove = false;
    public NPCMovement movement;
    public float movementTime = .5f;
    public float movementPause = 1.5f;

    private void Start()
    {
        movement.originalPoint = transform.position;
        cam = SceneManager.instance.cam.transform;
    }


    // Update is called once per frame
    void Update()
    {
        SetMovement();
        VerticalMovement();
        Move();
        AnimationStateCheck();
    }

    protected IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(movementTime);

        move = new Vector2(0f, 0f);

        yield return new WaitForSeconds(movementPause);
        canMove = true;
    }

    public override void SetMovement()
    {
        if (canMove)
        {
            move = movement.GetMovementVector(transform.position);

            canMove = false;
            StartCoroutine(WaitToMove());
        }

        base.SetMovement();
    }

    public override void Move()
    {
        Vector3 moveDir = movement.ApplyMovementVector(direction);
        animator.SetFloat("Speed", currentSpeed / 11);

        controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        runDirection = directionMultiplier * Vector3.Angle(moveDir, Camera.main.transform.forward);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {

        }
    }

    protected void OnEnable()
    {
        StartCoroutine(WaitToMove());
    }

    protected void OnDisable()
    {
        canMove = false;
        move = Vector2.zero;
    }
}
