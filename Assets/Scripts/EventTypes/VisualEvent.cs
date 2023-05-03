using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEvent : MonoBehaviour
{
    public Transform endPoint;
    public float duration;
    [HideInInspector] public bool activate;

    public void Activate()
    {
        LeanTween.move(this.gameObject, endPoint.position, duration).setEase(LeanTweenType.easeOutCirc);
        // StartCoroutine(CRT_Activate());
    }

    // IEnumerator CRT_Activate()
    // {
    //     yield return null;
    // }
}
