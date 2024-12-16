using UnityEngine;

public class CircleMovement : NPCMovement
{
    float angle = 0f, angularSpeed = 2f;

    private void Start()
    {
        originalPoint = transform.position - new Vector3(2, 0, 0);
    }
    public override Vector2 GetMovementVector(Vector3 currentPos)
    {
        var xVal = originalPoint.x + Mathf.Cos(angle) * movementRadius;
        var zVal = originalPoint.z + Mathf.Sin(angle) * movementRadius;
        angle = angle + Time.deltaTime * angularSpeed;
        Vector3 target = new Vector3(xVal, 0f, zVal);
        Vector3 toMove = (target - currentPos).normalized;
        if (angle >= 360f)
            angle = 0f;

        return new Vector2(toMove.x, toMove.z);
    }
}
