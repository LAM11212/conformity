using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "Scriptable Objects/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public string[] lines;
    public float typeSpeed = 0.3f;
}
