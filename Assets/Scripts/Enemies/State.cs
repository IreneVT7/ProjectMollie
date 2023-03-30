using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATES
    {
        IDLE, PATROL, ROOMPATROL, CHASE, ATTACK
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
            HannahStateManager.instance.DetectCharacterAudio();
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
            HannahStateManager.instance.rooms = GameObject.FindGameObjectsWithTag("Room");
            Debug.Log("Patrol");
            if (HannahStateManager.instance.agent.remainingDistance <= .1f)
            {
                HannahStateManager.instance.Patrol();
            }
            base.Start();
        }

        public override void Update()
        {
            HannahStateManager.instance.DetectCharacter();
            HannahStateManager.instance.DetectCharacterAudio();
            HannahStateManager.instance.RoomDetection();
            if (HannahStateManager.instance.agent.remainingDistance <= .1f)
            {
                nextState = new RoomPatrol();
                stage = EVENTS.EXIT;
            }

            if (HannahStateManager.instance.target != null)
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
            Debug.Log("Room Patrol");
            base.Start();
            counter = 10f;
        }

        public override void Update()
        {
            HannahStateManager.instance.RoomPatrol();
            HannahStateManager.instance.DetectCharacter();
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
            HannahStateManager.instance.anim.SetTrigger("isChasing");
            base.Start();
        }
        public override void Update()
        {
            HannahStateManager.instance.DetectCharacter();
            HannahStateManager.instance.DetectCharacterAudio();
            HannahStateManager.instance.Chase();
            if (HannahStateManager.instance.target == null)
            {
                nextState = new Patrol();
                stage = EVENTS.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
