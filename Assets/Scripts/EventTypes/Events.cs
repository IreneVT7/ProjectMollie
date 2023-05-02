using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Events : MonoBehaviour
{
    public enum EventType
    {
        AUDIO, VISUAL, FALL, STORY
    }
    public EventType type;
    private AudioSource auSource;
    public AudioClip clip;
    public int dialogueNum;
    public GameObject eventObject;
    [HideInInspector] public bool eventStart;


    // Update is called once per frame
    void Update()
    {
        if (eventStart)
        {
            if (type == EventType.AUDIO)
            {
                auSource.PlayOneShot(clip);
            }
            else if (type == EventType.VISUAL)
            {
                //se activa objeto y su codigo hace lo suyo
            }
            else if (type == EventType.FALL)
            {
                //se activa objeto y su codigo hace lo suyo
            }
            else if (type == EventType.STORY)
            {
                DialogueUI.instance.ShowDialogue(dialogueNum);
            }
            eventStart = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        eventStart = true;
        Destroy(this.gameObject, 0.1f);

    }
}
