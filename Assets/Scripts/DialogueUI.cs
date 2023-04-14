using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text textLabel;
    public TypewritterEffect TypewritterEffect;
    [SerializeField] private DialogueObject testDialogue;

    // Start is called before the first frame update
    void Start()
    {
        ShowDialogue(testDialogue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return TypewritterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        }
        OpenAndCloseDialogueBox(false);
    }

    public void OpenAndCloseDialogueBox(bool opened)
    {
        if (opened)
        {
            dialogueBox.SetActive(true);
        }
        else
        {
            dialogueBox.SetActive(false);
            textLabel.text = string.Empty;
        }
    }
}
