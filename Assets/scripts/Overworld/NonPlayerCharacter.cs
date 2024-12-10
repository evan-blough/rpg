using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public Transform cam;
    public SpriteRenderer sprite;
    public Animator animator;
    protected float speed = 10f;
    protected float currentSpeed;
    public float animDirection;
    protected float gravity = -9.81f;

    float movementRadius;
    Vector3 originalPoint;

    private void Start()
    {
        originalPoint = transform.position;
        cam = GameManager.instance.cam.transform;
    }


    // Update is called once per frame
    void Update()
    {
        sprite.gameObject.transform.rotation = Camera.main.transform.rotation;

        Move();
        var angleDif = CameraHandler.AngleNormalization(sprite.transform.eulerAngles.y);

        animDirection = GetDirection(transform.eulerAngles.y - angleDif);
        animator.SetFloat("Direction", animDirection);

        if (animDirection <= 7 && animDirection >= 5)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
    public float GetDirection(float angle)
    {
        // keep angle between the bounds
        angle = CameraHandler.AngleNormalization(angle);

        // north
        if ((angle >= -22.5f && angle <= 0f) || (angle >= 0f && angle < 22.5f)) return 0f;
        //northeast
        else if (angle >= 22.5f && angle < 67.5f) return 1f;
        //east
        else if (angle >= 67.5f && angle < 112.5f) return 2f;
        //southeast
        else if (angle >= 112.5f && angle < 157.5f) return 3f;
        //southwest
        else if (angle >= -157.5f && angle < -112.5f) return 5f;
        //west
        else if (angle >= -112.5f && angle < -67.5f) return 6f;
        //northwest
        else if (angle >= -67.5f && angle < -22.5f) return 7f;
        //south
        else return 4f;
    }
    public virtual void Move()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {

        }
    }
}
