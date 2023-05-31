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
    public static Vector3 previousPos = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (ClimbingHand)
        {
            Climb();
        }
        else
        {
            if (!characterController.isGrounded)
            {
                characterController.SimpleMove(new Vector3());
            }
        }
    }

    private void Climb()
    {
        Vector3 position = ClimbingHand.positionAction.action.ReadValue<Vector3>();
        Vector3 velocity = (position - previousPos) / Time.fixedDeltaTime;
        characterController.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
        previousPos = position;
    }
}
