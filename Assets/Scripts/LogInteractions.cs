using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LogInteractions : MonoBehaviour
{
    public void LogHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log(gameObject.name + " Hover Enter by: " + args.interactorObject.transform.gameObject.name);
    }

    public void LogHoverExit(HoverExitEventArgs args)
    {
        Debug.Log(gameObject.name + " Hover Exit by: " + args.interactorObject.transform.gameObject.name);
    }

    public void LogSelect(SelectEnterEventArgs args)
    {
        Debug.Log(gameObject.name + " Selected by: " + args.interactorObject.transform.gameObject.name);
    }

    public void LogDeselect(SelectExitEventArgs args)
    {
        Debug.Log(gameObject.name + " Deselected by: " + args.interactorObject.transform.gameObject.name);
    }

    public void LogActivate(ActivateEventArgs args)
    {
        Debug.Log(gameObject.name + " Activated by: " + args.interactorObject.transform.gameObject.name);
    }

    public void LogDeactivate(DeactivateEventArgs args)
    {
        Debug.Log(gameObject.name + " Deactivated by: " + args.interactorObject.transform.gameObject.name);
    }
}
