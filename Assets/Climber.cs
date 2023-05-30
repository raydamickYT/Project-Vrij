using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Climber : MonoBehaviour
{
    private CharacterController characterController;
    public static ActionBasedController ClimbingHand;
    private Vector3 previousPos = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (ClimbingHand)
        {
            Climb();
        }
        else
        {
            previousPos = Vector3.zero;
        }
    }

    private void Climb()
    {
        Vector3 position = ClimbingHand.positionAction.action.ReadValue<Vector3>();
        if (previousPos == Vector3.zero)
        {
            previousPos = position;
        }
        Vector3 velocity = (position - previousPos) / Time.deltaTime;
        characterController.Move(-velocity * Time.deltaTime);
        previousPos = position;
        Debug.Log(velocity);
    }
}
