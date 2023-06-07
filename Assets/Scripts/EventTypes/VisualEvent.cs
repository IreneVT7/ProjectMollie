using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEvent : MonoBehaviour
{
    public Transform endPoint;
    public float duration;

    public void Activate()
    {
        LeanTween.move(this.gameObject, endPoint.position, duration).setEase(LeanTweenType.easeOutCirc);
    }
}
