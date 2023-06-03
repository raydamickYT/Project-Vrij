using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbProvider : MonoBehaviour
{
    public static event Action ClimbActive;
    public static event Action ClimbInActive;

    private CharacterController characterController;
    [SerializeField] private InputActionProperty posRight;
    [SerializeField] private InputActionProperty posLeft;

    private Vector3 previousPos = Vector3.zero;

    private bool rightActive = false;
    private bool leftActive = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        DirectClimbInteractor.ClimbingHandActivated += HandActivated;
        DirectClimbInteractor.ClimbingHandDeactivated += HandDeactivated;
    }

    private void OnDestroy()
    {
        DirectClimbInteractor.ClimbingHandActivated -= HandActivated;
        DirectClimbInteractor.ClimbingHandDeactivated -= HandDeactivated;
    }

    private void HandActivated(string controllerName)
    {
        if (controllerName == "LeftHand")
        {
            leftActive = true;
            rightActive = false;
        }
        else
        {
            leftActive = false;
            rightActive = true;
        }

        ClimbActive?.Invoke();
    }

    private void HandDeactivated(string controllerName)
    {
        if (rightActive && controllerName == "RightHand")
        {
            rightActive = false;
            ClimbInActive?.Invoke();
        }
        if (leftActive && controllerName == "LeftHand")
        {
            leftActive = false;
            ClimbInActive?.Invoke();
        }
    }

    void FixedUpdate()
    {
        if (rightActive || leftActive)
        {
            Debug.Log("Climb");
            Climb();
        }
        else
        {
            previousPos = Vector3.zero;
        }
    }

    private void Climb()
    {
        Vector3 position = leftActive ? posLeft.action.ReadValue<Vector3>() : posRight.action.ReadValue<Vector3>();
        Debug.Log(position);
        if (previousPos == Vector3.zero)
        {
            previousPos = position;
        }
        Vector3 velocity = (position - previousPos) / Time.fixedDeltaTime;
        characterController.Move(-velocity * Time.fixedDeltaTime);
        previousPos = position;
    }
}
