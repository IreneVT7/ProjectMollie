using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChanger : MonoBehaviour
{
    public Transform Player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RoomFarFromPlayer")
        {
            other.gameObject.tag = "Room";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Room")
        {
            other.gameObject.tag = "RoomFarFromPlayer";
        }
    }

}
