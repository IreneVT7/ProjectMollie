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
        LeanTween.rotateX(gameObject, -344, 0f);
        LeanTween.rotateY(gameObject, 171, 0f);
        LeanTween.rotateZ(gameObject, -237, 0f);
        MoveGordon();
    }

    public void MoveGordon()
    {
        StartCoroutine(CRT_RollingStones());
    }

    IEnumerator CRT_RollingStones()
    {
        yield return new WaitForSeconds(1f);

        LeanTween.rotateX(gameObject, -193, .3f);
        LeanTween.rotateY(gameObject, -52, .3f);
        LeanTween.rotateZ(gameObject, -53, .3f);

    }
}
