using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaurenAIBehaviour : MonoBehaviour
{
    public Transform[] rooms;
    public float hearingRange = 5f;
    public float maxDistance = 20;
    public LayerMask targetLayer;
    public LayerMask wallLayer;
    bool foundPlayer = false;
    bool playerLeft = true;
    bool attackPlayer = false;
    float aux1, aux2;

    public static LaurenAIBehaviour instance;
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
        //spawnea en la habitacion inicial. despues ya selecciona habitacion random
        transform.position = rooms[0].position + new Vector3(0f, transform.position.y, 0f);
        ChangeRange();
    }


    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        if (foundPlayer)
        {
            //si se sale del rango que cambie de habitacion
            if (playerLeft)
            {
                StartCoroutine(CRT_ChangeRoom());
            }
            //si el jugador no esta agachado dentro del rango, que lo ataque
            if (true)
            {

            }
        }
        //se cambia al final del frame para que no lo repita multiples veces
        playerLeft = false;
    }


    void DetectPlayer()
    {
        Collider[] _targets = Physics.OverlapSphere(transform.position, hearingRange, targetLayer);
        if (_targets.Length > 0)
        {
            foundPlayer = true;
        }
        else
        {
            if (foundPlayer)
            {
                playerLeft = true;
            }
            foundPlayer = false;
        }
    }

    IEnumerator CRT_ChangeRoom()
    {
        yield return new WaitForSeconds(3f);
        ChangeRoom();
    }

    void ChangeRoom()
    {
        //selecciona habitacion random
        int r = Random.Range(0, rooms.Length);
        transform.position = rooms[r].position + new Vector3(0f, transform.position.y, 0f); ;
        ChangeRange();
    }

    void ChangeRange()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, maxDistance, wallLayer))
        {
            aux1 = (transform.position - hit.transform.position).sqrMagnitude;
        }
        if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit2, maxDistance, wallLayer))
        {
            aux2 = (transform.position - hit2.transform.position).sqrMagnitude;
        }

        if (aux1 <= aux2)
        {
            hearingRange = aux1;
        }
        else
        {
            hearingRange = aux2;
        }
        Debug.Log(hearingRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
