using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    public enum ObjectType
    {
        GORDON, NINA, DOOR
    }
    public ObjectType type;
    public GameObject eventTrigger;
    [HideInInspector] public bool interacted;


    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            if (type == ObjectType.GORDON)
            {
                if (!GameManager.instance.hasBiberon)
                {
                    DialogueUI.instance.ShowDialogue(0);
                }
                else if (GameManager.instance.biberonGiven)
                {
                    DialogueUI.instance.ShowDialogue(1);
                }
                else
                {
                    GameManager.instance.biberonGiven = true;
                    GameManager.instance.NotifyEvent("Biberon Quest Completado");
                    eventTrigger.SetActive(true);
                }
            }
            else if (type == ObjectType.NINA)
            {
                if (!GameManager.instance.hasTrain)
                {
                    DialogueUI.instance.ShowDialogue(2);
                }
                else if (GameManager.instance.trainGiven)
                {
                    DialogueUI.instance.ShowDialogue(4);
                }
                else
                {
                    DialogueUI.instance.ShowDialogue(3);
                    GameManager.instance.ShowKey();
                    GameManager.instance.trainGiven = true;
                    GameManager.instance.NotifyEvent("Nina Quest Completado");

                }
            }
            else if (type == ObjectType.DOOR)
            {
                if (GameManager.instance.hasKey)
                {
                    GameManager.instance.NotifyEvent("Segundo Piso desbloqueado");
                    LeanTween.rotate(this.gameObject, new Vector3(-90, 0, 87.5f), 5.5f).setEase(LeanTweenType.easeOutCirc);
                }
            }
            interacted = false;
        }
    }
}
