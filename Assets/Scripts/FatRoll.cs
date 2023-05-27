using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatRoll : MonoBehaviour
{
    public static FatRoll instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.init(800); 
        
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
