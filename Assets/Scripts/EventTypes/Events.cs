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
    public VisualEvent VisualEvent;
    public FallEvent FallEvent;
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
                VisualEvent.Activate();
            }
            else if (type == EventType.FALL)
            {
                FallEvent.Activate();
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
        if (other.gameObject.CompareTag("Player"))
        {
            eventStart = true;
            EventManager.instance.NextEvent();
            Destroy(this.gameObject, 0.1f);
        }
    }
}
