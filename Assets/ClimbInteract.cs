using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteract : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (args.interactorObject is XRDirectInteractor)
        {
            ActionBasedController controller = args.interactorObject.transform.GetComponent<ActionBasedController>();
            Climber.ClimbingHand = controller;
            Climber.previousPos = controller.positionAction.action.ReadValue<Vector3>();
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (args.interactorObject is XRDirectInteractor)
        {
            if (Climber.ClimbingHand && Climber.ClimbingHand.name == args.interactorObject.transform.name)
            {
                Climber.ClimbingHand = null;
            }
        }
    }
}
