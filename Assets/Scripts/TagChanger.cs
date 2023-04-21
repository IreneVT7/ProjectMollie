using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChanger : MonoBehaviour
{
    public Transform Player;
    public Transform centrePos;
    [SerializeField] private SphereCollider collider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!BasicCharacterStateMachine.instance.hiding)
        {
            transform.position = Player.position;
            collider.radius = 30;
        }
        else
        {
            StartCoroutine(CurrentRoom());  
        }
        
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

    IEnumerator CurrentRoom()
    {
        transform.position = centrePos.position;
        collider.radius = 10;
        yield return new WaitForSeconds(5f);
        collider.radius = 50;
    }
}
