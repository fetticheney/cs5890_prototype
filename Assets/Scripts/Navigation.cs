using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class Navigation : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    private float defaultSpeed = 5;
    public float speed = 5;
    private XROrigin rig;
    private float gravityAcceleration = -9.81f;
    private float currentAcceleration = 0f;
    private bool isJumping = false;
    private bool wasJumpButtonPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);


        character.Move(direction * Time.fixedDeltaTime * speed);
        // Apply gravitational acceleration each fixedDeltaTime
        currentAcceleration += gravityAcceleration * Time.fixedDeltaTime;
        character.Move(Vector3.up * currentAcceleration * Time.fixedDeltaTime);

        // Set acceleration to 0 if grounded
        if (character.isGrounded)
        {
            currentAcceleration = 0f;
            isJumping = false;
            wasJumpButtonPressed = false;
        }

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        bool jumpButtonPressed = false;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out jumpButtonPressed) && jumpButtonPressed)
        {
            if (!wasJumpButtonPressed && !isJumping)
            {
                // Perform jump
                isJumping = true;
                currentAcceleration += CalculateJumpSpeed();
            }
        }
        wasJumpButtonPressed = jumpButtonPressed;

        // Increase movement speed while trigger is held down
        float triggerValue = 0f;
        if (device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
        {
            if (triggerValue > 0)
            {
                // Modify speed when trigger button is held
                speed = defaultSpeed * 2; // Increase speed when trigger button is held
            }
            else
            {
                // Reset speed when trigger button is released
                speed = defaultSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    float CalculateJumpSpeed()
    {
        // h = (v^2) / (2 * g), where h is the desired jump height, v is the jump speed, and g is gravity.
        float jumpHeight = 2.0f;
        return Mathf.Sqrt(2 * Mathf.Abs(gravityAcceleration) * jumpHeight);
    }

    public void SetSpeedSlow()
    {
        Debug.Log("set speed slow");
        defaultSpeed = 2.5f;

    }

    public void SetSpeedNormal()
    {
        defaultSpeed = 5f;
    }

    public void SetSpeedFast()
    {
        defaultSpeed = 10f;
    }

}
