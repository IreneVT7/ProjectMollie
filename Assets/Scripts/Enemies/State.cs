using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class State
{
    public enum STATES
    {
        IDLE, PATROL, ROOMPATROL, CHASE, ATTACK, PUKECHASE
    }

    public enum EVENTS
    {
        START, UPDATE, EXIT
    }

    public virtual void Start() { stage = EVENTS.UPDATE; }
    public virtual void Update() { stage = EVENTS.UPDATE; }
    public virtual void Exit() { stage = EVENTS.EXIT; }

    public STATES name;
    protected EVENTS stage;
    protected State nextState;
    protected float counter;

    public State()
    {

    }

    public State Process()
    {
        //Si el evento en el que estoy es el de entrada, hago el método correspondiente de entrada
        if (stage == EVENTS.START) Start();
        //Si el evento en el que estoy es el de update, hago el método correspondiente
        if (stage == EVENTS.UPDATE) Update();
        //Si el evento en el que estoy es el de salida, hago el método correspondiente
        if (stage == EVENTS.EXIT)
        {
            Exit();
            //Y devolvemos el siguiente estado al que ir
            return nextState;
        }
        //Devolvemos el resultado del método
        return this;
    }
    public class IdleHannah : State
    {
        public IdleHannah() : base()
        {           
            name = STATES.IDLE;
        }

        public override void Start()
        {
            Debug.Log("Idle Hannah");
            HannahStateManager.instance.Idle();
            base.Start();
        }

        public override void Update()
        {
            HannahStateManager.instance.DetectCharacter();
            if (HannahStateManager.instance.target != null)
            {
                nextState = new Chase();
                stage = EVENTS.EXIT;
            }
            else if (Random.Range(0, 100) < 10)
            {
                nextState = new Patrol();
                stage = EVENTS.EXIT;
            }
        }

        public override void Exit()
        {
            HannahStateManager.instance.anim.ResetTrigger("isIdle");
            base.Exit();
        }
    }
    public class Patrol : State
    {
        public Patrol() : base()
        {
            name = STATES.PATROL;
        }

        public override void Start()
        {
            HannahStateManager.instance.detected = false;
            HannahStateManager.instance.rooms = GameObject.FindGameObjectsWithTag("Room");
            Vector3 vector3 = HannahStateManager.instance.centrePoint.position = HannahStateManager.instance.rooms[HannahStateManager.instance.randomRoom].transform.position + new Vector3(0, 1, 0);
            Debug.Log("Patrol");
            if (HannahStateManager.instance.agent.remainingDistance <= .1f)
            {
                HannahStateManager.instance.Patrol();
            }
            base.Start();
        }

        public override void Update()
        {
            HannahStateManager.instance.RoomDetection();
            HannahStateManager.instance.DetectCharacter();

            HannahStateManager.instance.rooms = GameObject.FindGameObjectsWithTag("Room");
            HannahStateManager.instance.waypoints = GameObject.FindGameObjectsWithTag("WayPoints");
            if (HannahStateManager.instance.agent.remainingDistance <= 1f)
            {
                nextState = new RoomPatrol();
                stage = EVENTS.EXIT;
            }

            if (HannahStateManager.instance.target != null)
            {
                nextState = new Chase();
                stage = EVENTS.EXIT;
            }

            if (PukeBehavior.instance.detected)
            {
                nextState = new Chase();
                stage = EVENTS.EXIT;
            }
        }

        public override void Exit()
        {
            HannahStateManager.instance.anim.ResetTrigger("isWalking");
            base.Exit();
        }
    }
    public class RoomPatrol : State
    {
        public RoomPatrol() : base()
        {
            name = STATES.ROOMPATROL;
        }

        public override void Start()
        {
            HannahStateManager.instance.detected = false;
            WaypointLocator.instance.FirstWayPoint = new Vector3(Random.Range(-1, 1), 0, 1);
            WaypointLocator.instance.SecondWayPoint = new Vector3(Random.Range(1, -1), 0, -1);
            WaypointLocator.instance.ThirdWayPoint = new Vector3(1, 0, Random.Range(1, -1));
            WaypointLocator.instance.FourthWayPoint = new Vector3(-1, 0, Random.Range(1, -1));
            HannahStateManager.instance.waypoints = GameObject.FindGameObjectsWithTag("WayPoints");
            Debug.Log("Room Patrol");
            counter = 20f;
            base.Start();
        }

        public override void Update()
        {
            HannahStateManager.instance.RoomPatrol();
            HannahStateManager.instance.DetectCharacter();
            HannahStateManager.instance.waypoints = GameObject.FindGameObjectsWithTag("WayPoints");
            if (HannahStateManager.instance.target != null)
            {
                nextState = new Chase();
                stage = EVENTS.EXIT;
            }

            if (counter > .1f)
            {
                counter -= Time.deltaTime;
            }
            if (counter <= .1f)
            {
                nextState = new IdleHannah();
                stage = EVENTS.EXIT;
            }

            if (PukeBehavior.instance.detected)
            {
                nextState = new Chase();
                stage = EVENTS.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
    public class Chase : State
    {
        public Chase() : base()
        {
            name = STATES.CHASE;
        }

        public override void Start()
        {
            HannahStateManager.instance.detected = true;
            Debug.Log("Chase");
            HannahStateManager.instance.anim.SetTrigger("isChasing");
            base.Start();
        }
        public override void Update()
        {
            if (HannahStateManager.instance.detected)
            {
                if (!PukeBehavior.instance.detected)
                {
                    HannahStateManager.instance.DetectCharacter();
                }                
                HannahStateManager.instance.Chase();
                if (HannahStateManager.instance.target == null)
                {
                    nextState = new Patrol();
                    stage = EVENTS.EXIT;
                }

            }
            else if(HannahStateManager.instance.target == null)
            {
                nextState = new Patrol();
                stage = EVENTS.EXIT;
            }

            if (HannahStateManager.instance.GetDistanceToTarget() < HannahStateManager.instance.attackRange * HannahStateManager.instance.attackRange)
            {
                nextState = new Attack();
                stage = EVENTS.EXIT;
            }


        }

        public override void Exit()
        {
            base.Exit();
        }
    }
    public class Attack : State
    {
        public Attack(): base()
        {
            name = STATES.ATTACK;
        }
        public override void Start()
        {
            Debug.Log("Attack");
            HannahStateManager.instance.timeToAttack = HannahStateManager.instance.maxTime;
            HannahStateManager.instance.anim.SetTrigger("isAttacking");
            base.Start();
        }
        public override void Update()
        {
            HannahStateManager.instance.DetectCharacter();
            //HannahStateManager.instance.DetectCharacterAudio();
            HannahStateManager.instance.agent.velocity = Vector3.zero;
            if (HannahStateManager.instance.timeToAttack > 0f)
            {
                HannahStateManager.instance.timeToAttack -= Time.deltaTime;
            }
            if (HannahStateManager.instance.timeToAttack <= 0f)
            {
                HannahStateManager.instance.timeToAttack = HannahStateManager.instance.maxTime;
                SceneManager.LoadScene(1);
            }

            if (HannahStateManager.instance.GetDistanceToTarget() > HannahStateManager.instance.attackRange * HannahStateManager.instance.attackRange)
            {
                nextState = new Chase();
                stage = EVENTS.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

}
