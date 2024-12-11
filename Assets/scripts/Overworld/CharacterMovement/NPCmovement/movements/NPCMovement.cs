using UnityEngine;

public abstract class NPCMovement : MonoBehaviour
{
    public Vector3 originalPoint;
    public float movementRadius;
    public int movementFrequency;

    public abstract Vector2 GetMovementVector(Vector3 currentPos);
    public abstract Vector3 ApplyMovementVector(Vector3 direction);
}
