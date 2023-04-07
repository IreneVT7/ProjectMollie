using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> pickUps = new List<GameObject>();

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeterEnLista(GameObject pickUp, bool biberon)
    {
        pickUps.Add(pickUp);
    }
    public void QuitarDeLista(GameObject pickUp, bool biberon)
    {
        pickUps.Remove(pickUp);
    }

}
