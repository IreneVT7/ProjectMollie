using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatRoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.init(800);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.instance.biberonGiven)
        //{
            StartCoroutine(rollingStones());
        //}
    }

    IEnumerator rollingStones()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.moveZ(gameObject, -55, 3).setDelay(.4f);
        LeanTween.rotateZ(gameObject, -180, 3).setDelay(.4f);
        LeanTween.rotateX(gameObject, 0, .3f);
        LeanTween.moveY(gameObject, 2.5f, .3f);
        
    }
}
