using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool hasKey = false;
    [HideInInspector] public bool hasBiberon = false;
    [HideInInspector] public bool hasTrain = false;
    [HideInInspector] public bool hasPlank = false;
    [HideInInspector] public bool biberonGiven = false;
    [HideInInspector] public bool trainGiven = false;
    public GameObject key, floorPlank;
    public Animation UITaskAnimation;
    public TMP_Text foundText, TaskText;


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
        TaskText.text = "Explore Main Floor";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NotifyEvent(string pickedUpText, string newTaskText)
    {
        Debug.Log(pickedUpText);
        foundText.text = pickedUpText;
        UITaskAnimation.Play();
        StartCoroutine(CRT_TaskWriteDelay(newTaskText));
    }

    public void ShowKey()
    {
        key.SetActive(true);
    }
    public void ShowPlank()
    {
        floorPlank.SetActive(true);
    }

    IEnumerator CRT_TaskWriteDelay(string newTaskText)
    {
        yield return new WaitForSeconds(0.15f);
        TaskText.text = newTaskText;
    }
}
