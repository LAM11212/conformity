using UnityEngine;

public class startDialogue : MonoBehaviour
{
    public DialogueObject dialogueObject;
    public DialogueBox dialogueBox;
    public BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(dialogueObject != null)
            {
                dialogueBox.setDialogue(dialogueObject);
                dialogueBox.canAccessDialogue = true;
                dialogueBox.clearPrevDialogue();
                dialogueBox.StartDialogue();
                boxCollider.enabled = false;
            }
        }
    }
}
