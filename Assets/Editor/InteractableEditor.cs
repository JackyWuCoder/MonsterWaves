using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventsOnlyInteractable))
        {
            // interactable.SetPromptMessage(EditorGUILayout.TextField("Prompt Message ", interactable.GetPromptMessage()));
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents,", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        base.OnInspectorGUI();
        if (interactable.useEvents)
        {
            if (interactable.GetComponent<InteractionEvent>() == null)
                // We are using events, add the component.
                interactable.gameObject.AddComponent<InteractionEvent>();
        }
        else
        {
            // We are not using events, remove the component.
            if (interactable.GetComponent<InteractionEvent>() != null)
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());
        }
    }
}
