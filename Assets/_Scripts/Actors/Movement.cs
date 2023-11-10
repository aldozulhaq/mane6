using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxMoveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float deceleration = 20f;

    [Header("Events")]
    [SerializeField] FloatEventChannel playerSpeedChannel;
    [SerializeField] FloatEventChannel playerRotationChannel;

    [Header("Weapon Parents")]
    public Transform FollowPos;
    public Transform OrbitPos;

    Vector3 previousDir = Vector3.up;
    float currentMoveSpeed = 0f;

    private void Update()
    {
        HandleMovementInput();
        SendSpeedToAnimator();
        playerRotationChannel.Invoke(transform.localEulerAngles.y);
    }

    private void HandleMovementInput()
    {
        float horizontalMov = Input.GetAxisRaw("Horizontal");
        float verticalMov = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(horizontalMov, 0f, verticalMov).normalized;

        if (moveDir != Vector3.zero)
        {
            RotateCharacter(moveDir);
            ApplyAcceleration(moveDir);
        }
        else
        {
            ApplyDeceleration();
        }
    }

    private void RotateCharacter(Vector3 moveDir)
    {
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyAcceleration(Vector3 moveDir)
    {
        currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, maxMoveSpeed, acceleration * Time.deltaTime);
        transform.parent.Translate(moveDir * currentMoveSpeed * Time.deltaTime, Space.World);
        previousDir = moveDir;
    }

    private void ApplyDeceleration()
    {
        currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, 0f, deceleration * Time.deltaTime);
        transform.parent.Translate(previousDir * currentMoveSpeed * Time.deltaTime, Space.World);
    }
    // Sends the current movement speed to the animator via an event channel.
    private void SendSpeedToAnimator()
    {
        playerSpeedChannel.Invoke(currentMoveSpeed);
    }
}
