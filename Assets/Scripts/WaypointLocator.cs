using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointLocator : MonoBehaviour
{
    public GameObject[] waypoints;

    public float maximumDistance;
    public float distance;

    public LayerMask WallLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayCasts();
    }

    public void rayCasts()
    {

        Debug.DrawRay(transform.position + Vector3.back * maximumDistance /2, Vector3.down, Color.red);

        Debug.DrawRay(transform.position + Vector3.right * maximumDistance, Vector3.down, Color.red);

        Debug.DrawRay(transform.position + Vector3.left * maximumDistance, Vector3.down, Color.red);

        Debug.DrawRay(transform.position + Vector3.back * maximumDistance, Vector3.down, Color.red);


        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, maximumDistance))
        {

        }
        if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit2, maximumDistance))
        {

        }
        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit3, maximumDistance))
        {

        }
        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hit4, maximumDistance))
        {

        }

        if (Physics.Raycast(hit.point + Vector3.back * distance, Vector3.down, out RaycastHit _hit)) 
        {
            waypoints[0].transform.position = _hit.point;
        }
        if (Physics.Raycast(hit2.point + Vector3.left * distance, Vector3.down, out RaycastHit _hit2))
        {
            waypoints[1].transform.position = _hit2.point;
        }
        if (Physics.Raycast(hit3.point + Vector3.right * distance, Vector3.down, out RaycastHit _hit3)) 
        {
            waypoints[2].transform.position = _hit3.point;
        }
        if (Physics.Raycast(hit4.point + Vector3.forward * distance, Vector3.down, out RaycastHit _hit4))
        {
            waypoints[3].transform.position = _hit4.point;
        }

    }

    private void OnDrawGizmos()
    {
        
    }
}
