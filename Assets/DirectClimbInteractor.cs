using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectClimbInteractor : XRDirectInteractor
{
    public static event Action<string> ClimbingHandActivated;
    public static event Action<string> ClimbingHandDeactivated;

    private string controllerName;

    protected override void Start()
    {
        base.Start();
        controllerName = gameObject.name;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        Debug.Log(args.interactorObject.transform.gameObject.layer);
        if (args.interactorObject.transform.gameObject.layer == 3)
        {
            UnityEngine.Debug.Log("Add Hand");
            ClimbingHandActivated?.Invoke(controllerName);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ClimbingHandDeactivated?.Invoke(controllerName);
    }
}
