using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEvent : MonoBehaviour
{
    public Transform endPoint;
    public GameObject shadow;
    public float duration;

    public void Activate()
    {
        shadow.SetActive(true);
        SoundManager.instance.PlayOneshot(0, EventManager.instance.laugh);
        LeanTween.move(shadow, endPoint.position, duration).setEase(LeanTweenType.easeOutCirc);
    }

    public void DeactivateShadow()
    {
        shadow.SetActive(false);
    }
}
