using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeBehavior : MonoBehaviour
{
    public GameObject Puke;
    GameObject instantiatedPuke;
    public bool instantiated;
    public bool detected;

    //public AudioSource audio;

    public Transform[] PukePlaces;
    // Start is called before the first frame update
    public static PukeBehavior instance;

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InstantiatePuke();
    }

    public void InstantiatePuke()
    {
        
        if (!instantiated)
        {
            //audio.Play();
            instantiatedPuke = Instantiate(Puke);
            instantiatedPuke.transform.position = PukePlaces[Random.Range(0, 4)].position;
            instantiated = true;
        }
    }
}
