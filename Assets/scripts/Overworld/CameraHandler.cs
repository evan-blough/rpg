using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    PlayerControls controls;

    public CinemachineFreeLook cam;
    public float targetRotation;
    public float currentRotation;
    public float rotationSpeed = 5f;
    void Awake()
    {
        GameManager.instance.cam = this;
        controls = ControlsHandler.instance.playerControls;

        controls.overworld.CameraRotation.performed += ctx => RotateCamera(ctx.ReadValue<float>());
    }

    public void RotateCamera(float rotateDirection)
    {
        targetRotation += 45 * (rotateDirection == 0 ? 0 : (rotateDirection < 0 ? -1 : 1));
        if (targetRotation > 180)
        {
            targetRotation = -135;
        }
        else if (targetRotation < -180)
        {
            targetRotation = 135;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(currentRotation - targetRotation) > 0.1f)
        {
            currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

            currentRotation = AngleNormalization(currentRotation);

            cam.m_XAxis.Value = currentRotation;
        }
    }

    // normalizes the angle to a scale of -180 => 180 degrees due to that being the range of transform.eulerAngles
    public static float AngleNormalization(float angle)
    {
        float newAngle = angle;

        if (newAngle > 180) newAngle = (newAngle % 180) + (-180);
        else if (newAngle < -180) newAngle = 180 - (newAngle % 180);

        return newAngle;
    }
}
