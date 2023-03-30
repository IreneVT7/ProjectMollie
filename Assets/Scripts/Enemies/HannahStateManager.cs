using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static State;

public class HannahStateManager : MonoBehaviour
{
    public float speed = 5f;
    public float chasingSpeed = 8f;
    public float chaseDuration;

    public static HannahStateManager instance;
    public Animator anim;
    public NavMeshAgent agent;

    #region detectionVAR
    [Header("Deteccion")]
    public float visionRange;
    public float visionAngle;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;
    public Transform rayOrigin;
    public Transform target;
    public Transform TransformL, TransformR;
    #endregion
    #region patrolVAR
    [Header("Patrullaje")]
    public GameObject[] rooms;
    public float counter = 10f;
    public Transform centrePoint;
    public float range;
    public int randomRoom;
    public bool isInsideRoom;
    #endregion


    public State currentState;
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

    private void Start()
    {
        currentState = new IdleHannah();
        agent.autoBraking = false;
        agent.velocity = Vector3.zero;
        
        isInsideRoom = false;
        agent.speed = speed;
    }
    private void Update()
    {
        currentState = currentState.Process();
        rooms = GameObject.FindGameObjectsWithTag("Room");
    }
    public void Idle()
    {
        anim.SetTrigger("isIdle");
    }

    #region Detection
    public void DetectCharacter()
    {
        //Guardamos todos los objetos encontrados con el overlap
        Collider[] _targets = Physics.OverlapSphere(transform.position, visionRange, targetLayer);
        //Si ha encontrado algún objeto, la longitud del array es mayor que 0
        if (_targets.Length > 0)
        {
            //Calculamos la direccion hacia el objeto
            Vector3 _targetDir = _targets[0].transform.position - rayOrigin.position;
            //Si esta fuera del angulo de vision, lo ignoramos
            //Se calcula si esta dentro con el angulo que hay entre el forward y la direccion
            //del objetivo. Si este angulo es menor que la mitad del angulo de vision, esta dentro
            if (Vector3.Angle(transform.forward, _targetDir) > visionAngle / 2f)
            {
                return;
            }
            //Lanzamos un rayo desde el enemigo hacia el jugador para comprobar si esta
            //escondido detras de alguna pared u obstaculo
            //Sumamos un offset al origen en el eje Y para que no lance el rayo desde los pies
            if (Physics.Raycast(rayOrigin.position, _targetDir.normalized,
                _targetDir.magnitude, obstacleLayer) == false)
            {
                target = _targets[0].transform;
            }
            //Dibujamos el rayo que comprueba si esta tras un obstaculo
            //Sumamos un offset al origen en el eje Y para que no lance el rayo desde los pies
            Debug.DrawRay(rayOrigin.position, _targetDir, Color.magenta);
        }
        //Si el array está vacío, no ha encontrado nada
        else
        {
            StartCoroutine(stopChasing());
        }
    }

    public void DetectCharacterAudio()
    {
        //Guardamos los objetos encontrados en el array con overlapsphere
        Collider[] _targets = Physics.OverlapSphere(transform.position, visionRange, targetLayer);
        if (_targets.Length > 0)
        {
            Debug.Log("SE HA DETECTADO " + _targets.Length + " ENEMIGO");
            Vector3 _targetDir = _targets[0].transform.position - rayOrigin.position;
            if (BasicCharacterStateMachine.instance.moveSpeed <= 4f)
            {
                //Lanzamos un rayo desde el enemigo hacia el jugador para comprobar si esta
                //escondido detras de alguna pared u obstaculo
                //Sumamos un Offset al origen en el eje Y para que no lance el rayo desde los pies
                if (Physics.Raycast(rayOrigin.position, _targetDir.normalized,
                    _targetDir.magnitude, obstacleLayer) == false)
                {
                    target = _targets[0].transform;
                }

            }
            //Dibujamos el rayo que comprueba si esta tras un obstaculo
            //Sumamos un offset al origen en el eje Y para que no lance el rayo desde los pies
            Debug.DrawRay(rayOrigin.position, _targetDir, Color.blue);
        }
    }

    public void RoomDetection()
    {
        //Escoge una localizacion dentro del array
        randomRoom = Random.Range(0, rooms.Length);
        if (rooms.Length <= 0)
        {
            return;
        }
        //La posicion del centro de la habitacion se coloca en la posicion escogida
        centrePoint.position = rooms[randomRoom].transform.position;
    }
    #endregion

    #region Patrolling
    public void Patrol()
    {
        if (rooms.Length <= 0)
        {
            return;
        }
        //Se establece el destino del agente
        agent.SetDestination(rooms[randomRoom].transform.position);
        //Se activa la animacion de andar
        anim.SetTrigger("isWalking");
    }

    public void RoomPatrol()
    {
        if (agent.remainingDistance <= .1f)
        {
            Vector3 point;
            if (randomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.red, 1f);
                agent.SetDestination(point);
            }
        }
    }
    bool randomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 RandomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(RandomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    #endregion

    public void Chase()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.speed = chasingSpeed;
            if (agent.remainingDistance <= 1f)
            {
                agent.velocity = Vector3.zero;
            }
        }
    }

    IEnumerator stopChasing()
    {
        yield return new WaitForSeconds(chaseDuration);
        target = null;
    }

    private void OnDrawGizmos()
    {
        //Dibujamos el rango de vision
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        //Dibujamos el cono de vision
        Gizmos.color = Color.green;
        //Rotamos los helper para que tengan la rotacion igual a la mitad del angulo de vision
        //Para dibujar el cono de vision, rotamos dos objetos vacios para luego lanzar un rayo
        //en el forward de cada uno de ellos y dibuje el cono
        TransformL.localRotation = Quaternion.Euler(0f, visionAngle / -2f, 0f);
        TransformR.localRotation = Quaternion.Euler(0f, visionAngle / 2f, 0f);
        Gizmos.DrawRay(TransformL.position, TransformL.forward * visionRange);
        Gizmos.DrawRay(TransformR.position, TransformR.forward * visionRange);
    }
}
