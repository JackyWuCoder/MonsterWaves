using UnityEditor;
using UnityEngine;

public class ExpandAllChildren : EditorWindow
{
    [MenuItem("Tools/Expand All Children")]
    static void Init()
    {
        ExpandAllChildren window = GetWindow<ExpandAllChildren>();
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Expand All"))
        {
            ExpandChildren(Selection.activeTransform);
        }
    }

    void ExpandChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            ExpandChildren(child);
        }
    }
}