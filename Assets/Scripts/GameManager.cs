using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool hasKey;
    [HideInInspector] public bool hasBiberon;
    [HideInInspector] public bool hasTrain;


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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NotifyEvent(string text)
    {
        Debug.Log(text);
    }


}
