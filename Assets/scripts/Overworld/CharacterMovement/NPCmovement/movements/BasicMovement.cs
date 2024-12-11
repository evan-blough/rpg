using UnityEngine;

public class BasicMovement : NPCMovement
{
    public override Vector2 GetMovementVector(Vector3 currentPos)
    {
        if (Random.Range(0, 10) <= movementFrequency)
        {
            if (Vector3.Distance(originalPoint, currentPos) > movementRadius)
            {
                var moveBackToOrigin = CameraHandler.AutoMoveRelativeToCamera(originalPoint, currentPos);
                return new Vector2(moveBackToOrigin.x + Random.Range(-.15f, .15f), moveBackToOrigin.y + Random.Range(-.15f, .15f));
            }
            else
                return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        else
        {
            return Vector2.zero;
        }
    }
    public override Vector3 ApplyMovementVector(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        return direction;
    }
}
