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
    [HideInInspector] public bool interacted;


    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            if (type == ObjectType.GORDON && GameManager.instance.hasBiberon)
            {
                GameManager.instance.hasBiberon = false;
                GameManager.instance.NotifyEvent("Biberon Quest Completado");
            }
            else if (type == ObjectType.NINA && GameManager.instance.hasTrain)
            {
                GameManager.instance.hasTrain = false;
                GameManager.instance.NotifyEvent("Nina Quest Completado");
            }
            else if (type == ObjectType.DOOR && GameManager.instance.hasKey)
            {
                GameManager.instance.hasKey = false;
                GameManager.instance.NotifyEvent("Segundo Piso desbloqueado");
            }
            interacted = false;
        }
    }
}
