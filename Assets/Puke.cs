using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puke : MonoBehaviour
{
    public float minDuration;
    public float maxDuration;
    public float duration;

    public bool detected;
    // Start is called before the first frame update
    void Start()
    {
        duration = Random.Range(minDuration, maxDuration);
    }

    private void Update()
    {
        if (duration >= 0)
        {
            duration -= Time.deltaTime;            
        }
        if (duration <= .1f)
        {
            PukeBehavior.instance.detected = false;
            Destroy(this.gameObject);
            PukeBehavior.instance.instantiated = false;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PukeBehavior.instance.detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PukeBehavior.instance.detected = false;
        }
    }
}
