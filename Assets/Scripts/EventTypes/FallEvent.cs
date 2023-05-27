using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEvent : MonoBehaviour
{
    public GameObject thisTrigger, secondTrigger;
    public Rigidbody[] rbs;

    private void Start()
    {
        thisTrigger.SetActive(true);
        secondTrigger.SetActive(false);
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = true;
        }
    }

    public void Activate()
    {
        StartCoroutine(CRT_Activate());
    }

    IEnumerator CRT_Activate()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = true;
            rbs[i].detectCollisions = false;
        }
        secondTrigger.SetActive(true);
    }
}
