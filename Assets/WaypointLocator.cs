using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointLocator : MonoBehaviour
{
    public GameObject[] waypoints;

    public float maximumDistance;
    public bool wallFound1;
    public bool wallFound2;
    public bool wallFound3;
    public bool wallFound4;

    public LayerMask WallLayer;
    RaycastHit hit;
    RaycastHit hit2;
    RaycastHit hit3;
    RaycastHit hit4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayCasts();

        Vector3 direction = waypoints[0].transform.position - transform.position;
        waypoints[0].transform.position = hit.point;
        waypoints[1].transform.position = hit2.point;
        waypoints[2].transform.position = hit3.point;
        waypoints[3].transform.position = hit4.point;
    }

    public void rayCasts()
    {
        wallFound1 = Physics.Raycast(transform.position, Vector3.forward, out hit, maximumDistance, WallLayer);
        wallFound2 = Physics.Raycast(transform.position, Vector3.back, out hit2, maximumDistance, WallLayer);
        wallFound3 = Physics.Raycast(transform.position, Vector3.right, out hit3, maximumDistance, WallLayer);
        wallFound4 = Physics.Raycast(transform.position, Vector3.left, out hit4, maximumDistance, WallLayer);

        if (wallFound1){Debug.DrawRay(transform.position, Vector3.forward, Color.green, maximumDistance);
            waypoints[0].SetActive(true);}
        else{waypoints[0].SetActive(false);}

        if (wallFound2){Debug.DrawRay(transform.position, Vector3.back, Color.green, maximumDistance);
            waypoints[1].SetActive(true);}
        else{waypoints[1].SetActive(false);}

        if (wallFound3)
        {Debug.DrawRay(transform.position, Vector3.right, Color.green, maximumDistance);
            waypoints[2].SetActive(true);}
        else{waypoints[2].SetActive(false);}

        if (wallFound4)
        {Debug.DrawRay(transform.position, Vector3.left, Color.green, maximumDistance);
            waypoints[3].SetActive(true);}
        else{waypoints[3].SetActive(false);}
        
    }

    private void OnDrawGizmos()
    {
        
    }
}
