using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject[] events;
    [HideInInspector] public int currentEvent;

    public static EventManager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }


        for (int i = 0; i < events.Length; i++)
        {
            events[i].SetActive(false);
        }
        events[0].SetActive(true);
        currentEvent = 0;
    }

    public void NextEvent()
    {
        for (int i = 0; i < events.Length; i++)
        {
            events[i].SetActive(false);
        }
        currentEvent++;
        events[currentEvent].SetActive(true);
    }
}
