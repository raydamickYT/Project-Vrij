using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteract : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (args.interactorObject is XRDirectInteractor && !Climber.ClimbingHand)
        {
            Climber.ClimbingHand = args.interactorObject.transform.GetComponent<ActionBasedController>();
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
