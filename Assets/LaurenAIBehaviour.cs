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
    [SerializeField] bool foundPlayer = false;
    [SerializeField] bool playerLeft = true;
    [SerializeField] bool attackPlayer = false;
    float aux;

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
        transform.position = rooms[0].position + new Vector3(0f, 0.15f, 0f);
        ChangeRange();
    }


    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        if (foundPlayer)
        {
            //si el jugador no esta agachado dentro del rango, que lo ataque
            if (!BasicCharacterStateMachine.instance.sneaking && (BasicCharacterStateMachine.instance.moveDirection != Vector3.zero))
            {
                attackPlayer = true;
                Debug.Log("BOOO");
            }
        }
        //si se sale del rango que cambie de habitacion
        else if (playerLeft)
        {
            StartCoroutine(CRT_ChangeRoom());
        }
        //se cambia al final del frame para que no lo repita multiples veces
        playerLeft = false;
        foundPlayer = false;

    }


    void DetectPlayer()
    {
        Collider[] _targets = Physics.OverlapSphere(transform.position, hearingRange, targetLayer);
        //si detecta al jugador que diga que lo ha encontrado
        if (_targets.Length > 0)
        {
            Debug.Log("paso");
            foundPlayer = true;
        }
        //si no lo detecta pero foundPlayer sigue siendo true, significa que justo se ha ido
        else
        {
            if (foundPlayer)
            {
                playerLeft = true;
            }
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
        transform.position = rooms[r].position + new Vector3(0f, 0.15f, 0f);
        ChangeRange();
    }

    void ChangeRange()
    {

        float minDistance = 9999999;
        for (int i = 0; i < 4; i++)
        {
            Vector3 direction = Vector3.forward;
            switch (i)
            {
                case 0:
                    direction = Vector3.back;
                    break;
                case 1:
                    direction = Vector3.right;
                    break;
                case 2:
                    direction = Vector3.left;
                    break;
                default:
                    break;
            }

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance, wallLayer))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 99999);
                aux = (transform.position - hit.point).sqrMagnitude;
                if (minDistance > aux)
                {
                    minDistance = aux;
                }
            }
        }

        hearingRange = Mathf.Sqrt(minDistance);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
