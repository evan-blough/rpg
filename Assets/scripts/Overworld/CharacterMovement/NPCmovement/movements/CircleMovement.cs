using UnityEngine;

public class CircleMovement : NPCMovement
{
    float angle = 0f;
    float speed { get { return movementRadius / 2f; } }
    public Transform whatever;

    private void Start()
    {
        mainCamera = Camera.main;
        originalPoint = new Vector3(transform.position.x - movementRadius, 0f, transform.position.z);
    }

    public override Vector2 GetMovementVector(Vector3 currentPos)
    {
        var xVal = originalPoint.x + Mathf.Cos(angle) * movementRadius;
        var zVal = originalPoint.z + Mathf.Sin(angle) * movementRadius;

        // never manually code a circular path again... hacky solution to maintain a distance equal to the 
        // radius from the fixed point

        angle += Time.deltaTime * speed / movementRadius;

        Vector3 target = new Vector3(xVal, 0f, zVal);
        Vector3 toMove = (target - currentPos).normalized * speed * Time.deltaTime;
        angle %= 360f;

        return new Vector2(toMove.x, toMove.z);
    }
}
