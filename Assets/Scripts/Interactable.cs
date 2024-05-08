using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Add or remove an InteractionEvent component to this gameobject.
    public bool useEvents;
    // The message displayed to player when looking at an interactable object.
    [SerializeField] private string promptMessage;

    // Template Method Design Pattern
    // This function will be called from the player.
    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    { 
        // We wont have any code written in this function.
        // This is a template function to be overridden by our subclasses.
    }

    public string GetPromptMessage() 
    {
        return promptMessage;
    }

    public void SetPromptMessage(string promptMessage)
    {
        this.promptMessage = promptMessage;
    }
}
