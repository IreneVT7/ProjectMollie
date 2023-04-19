using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointLocator : MonoBehaviour
{
    public GameObject[] waypoints;

    public float maximumDistance;

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
        Debug.DrawRay(transform.position, Vector3.forward * maximumDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.forward * maximumDistance, Vector3.down * 5f, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * maximumDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.right * maximumDistance, Vector3.down * 5f, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * maximumDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.left * maximumDistance, Vector3.down * 5f, Color.red);
        Debug.DrawRay(transform.position, Vector3.back * maximumDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.back * maximumDistance, Vector3.down * 5f, Color.red);
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit _hit, maximumDistance))
        {

        }
        else if (Physics.Raycast(transform.position + Vector3.forward * maximumDistance, Vector3.down, out _hit, 5f)) 
        {
            waypoints[0].transform.position = _hit.point;
        }

        if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit _hit2, maximumDistance))
        {

        }
        else if (Physics.Raycast(transform.position + Vector3.right * maximumDistance, Vector3.down, out _hit2, 5f))
        {
            waypoints[1].transform.position = _hit2.point;
        }

        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit _hit3, maximumDistance))
        {

        }
        else if (Physics.Raycast(transform.position + Vector3.left * maximumDistance, Vector3.down, out _hit3, 5f))
        {
            waypoints[2].transform.position = _hit3.point;
        }

        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit _hit4, maximumDistance))
        {

        }
        else if (Physics.Raycast(transform.position + Vector3.back * maximumDistance, Vector3.down, out _hit4, 5f))
        {
            waypoints[3].transform.position = _hit4.point;
        }

    }

    private void OnDrawGizmos()
    {
        
    }
}
