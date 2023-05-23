using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatRoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.biberonGiven)
        {
            StartCoroutine(rollingStones());
        }
    }

    IEnumerator rollingStones()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.moveZ(gameObject, -50, 1).setEaseOutSine();
        LeanTween.rotateZ(gameObject, 90, .3f);
        LeanTween.rotateX(gameObject, -360, 1f).setEaseOutSine();
    }
}
