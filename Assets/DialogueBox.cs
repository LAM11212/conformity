using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    //public string[] lines;
    public DialogueObject dialogueObject;
    public GameObject dialogueUI;
    public float textSpeed = 0.05f;
    public bool canAccessDialogue = false;
    private bool skippedLine = false;
    playerMovement pm;

    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        dialogueUI.SetActive(false);
        pm = Object.FindFirstObjectByType<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click(InputAction.CallbackContext ctx)
    {
        if (!canAccessDialogue) return;

        if(ctx.performed)
        {
            if(skippedLine)
            {
                skippedLine = false;
                return;
            }
            skippedLine = true;
            if (textComponent.text == dialogueObject.lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogueObject.lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        pm.enabled = false;
        dialogueUI.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    public void clearPrevDialogue()
    {
        textComponent.text = string.Empty;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialogueObject.lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < dialogueObject.lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            pm.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void setDialogue(DialogueObject newDialogue)
    {
        dialogueObject = newDialogue;
    }
}
