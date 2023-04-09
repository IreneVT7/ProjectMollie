using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public enum ObjectType
    {
        BIBERON, TRAIN, KEY
    }
    public ObjectType type;
    [HideInInspector] public bool interacted;


    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            if (type == ObjectType.BIBERON)
            {
                GameManager.instance.hasBiberon = true;
                GameManager.instance.NotifyEvent("Tenemos biberon");
            }
            else if (type == ObjectType.TRAIN)
            {
                GameManager.instance.hasTrain = true;
                GameManager.instance.NotifyEvent("Tenemos tren");

            }
            else
            {
                GameManager.instance.hasKey = true;
                GameManager.instance.NotifyEvent("Tenemos llave");

            }
            interacted = false;
            this.gameObject.SetActive(false);
        }
    }
}
