using UnityEngine;

public class Map : MonoBehaviour
{
    TagChanger tagChanger;

    void Start()
    {
        tagChanger = GetComponent<TagChanger>();
        tagChanger.ChangeTagOfGameObjectAndChildren(transform, "Wall");
    }
}