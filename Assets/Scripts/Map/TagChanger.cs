using UnityEngine;

public class TagChanger : MonoBehaviour
{
    // Function to change the tag of a GameObject and its children recursively
    void ChangeTagRecursively(Transform parent, string newTag)
    {
        // Change the tag of the current GameObject
        parent.gameObject.tag = newTag;

        // Iterate through all children
        foreach (Transform child in parent)
        {
            // Recursively call ChangeTagRecursively for each child
            ChangeTagRecursively(child, newTag);
        }
    }

    // Example function to call ChangeTagRecursively
    public void ChangeTagOfGameObjectAndChildren(Transform parent, string newTag)
    {
        ChangeTagRecursively(parent, newTag);
    }
}
