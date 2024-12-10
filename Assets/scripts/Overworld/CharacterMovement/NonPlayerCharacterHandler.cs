using System.Collections;
using UnityEngine;

public class NonPlayerCharacterHandler : CharacterHandler
{
    private bool canMove = false;

    public float movementRadius;
    protected Vector3 originalPoint = new Vector3();

    private void Start()
    {
        originalPoint = transform.position;
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
        yield return new WaitForSeconds(.5f);

        move = new Vector2(0f, 0f);

        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }

    public override void SetMovement()
    {
        if (canMove)
        {
            if (Random.Range(0, 2) == 0)
            {
                if (Vector3.Distance(originalPoint, transform.position) > movementRadius)
                {
                    var moveBackToOrigin = CameraHandler.AutoMoveRelativeToCamera(originalPoint, transform.position);
                    move = new Vector2(moveBackToOrigin.x + Random.Range(-.15f, .15f), moveBackToOrigin.y + Random.Range(-.15f, .15f));
                }
                else
                    move = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            else
            {
                move = Vector2.zero;
            }

            canMove = false;
            StartCoroutine(WaitToMove());
        }

        base.SetMovement();
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
