using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool hasKey = false;
    [HideInInspector] public bool hasBiberon = false;
    [HideInInspector] public bool hasTrain = false;
    [HideInInspector] public bool biberonGiven = false;
    [HideInInspector] public bool trainGiven = false;
    public GameObject key;
    public GameObject[] eventsToDeactivate;


    public static GameManager instance;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        key.SetActive(false);
        for (int i = 0; i < eventsToDeactivate.Length; i++)
        {
            eventsToDeactivate[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NotifyEvent(string text)
    {
        Debug.Log(text);
    }

    public void ShowKey()
    {
        key.SetActive(true);
    }



}
