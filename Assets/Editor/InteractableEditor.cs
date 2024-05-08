using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        base.OnInspectorGUI();
        if (interactable.GetIsUsingEvents())
        {
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                // We are using events, add the component.
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
            else {
                // We are not using events, remove the component.
                if (interactable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
