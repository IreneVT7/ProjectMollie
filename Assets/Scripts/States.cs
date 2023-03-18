using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class States
{
    //enum con las cosas que el personaje puede hacer
    public enum STATE
    {
        IDLE, RUN, FALL, SNEAK, HIDE, PICKUP, INTERACT
    };

    //Estado actual del personaje
    public STATE stateName;

    //Constructor de la clase para un estado. 
    public States()
    {

    }

    //Estas líneas de métodos sobreescriben los eventos del estado, sea cual sea
    public virtual void Enter()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void Exit()
    {

    }


}

[SerializeField]
public class Idle : States
{
    //Usamos el constructor y le pasamos todas las variables que necesita
    public Idle() : base()
    {
        //Se le pasa el nombre del estado que tiene que hacer
        stateName = STATE.IDLE;
    }

    //ENTER
    public override void Enter()
    {
        // Debug.Log("entra");
        //Hacemos la animación de Idle
        // anim.SetTrigger("isIdle");
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
    public override void Update()
    {
        //Tiene que hacer el movimiento en idle aunque no haya input para asi saber cuando cambia su velocidad.
        BasicCharacterStateMachine.instance.MovementInput(BasicCharacterStateMachine.instance.moveSpeed);
        BasicCharacterStateMachine.instance.Grounded();

        if (BasicCharacterStateMachine.instance.moveDirection != Vector3.zero)
        {
            //Si se mueve pasamos a correr
            BasicCharacterStateMachine.instance.TransitionToState(new Run());
        }
        else if (BasicCharacterStateMachine.instance.pickingUp == true)
        {
            //Si esta cerca de algo y pulsamos el boton pasamos a recogerlo
        }
        else if ((Input.GetButton("Sneak")) && (BasicCharacterStateMachine.instance.isGrounded == true))
        {
            //Si snekea pos que sneakee
            BasicCharacterStateMachine.instance.TransitionToState(new Sneak());
        }
        else if (BasicCharacterStateMachine.instance.isGrounded == false)
        {
            //Si no detecta suelo entonces se cae
            BasicCharacterStateMachine.instance.TransitionToState(new Fall());
        }

        base.Update();

    }

    //EXIT 
    public override void Exit()
    {
        //Para evitar errores de animación
        // anim.ResetTrigger("isIdle");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

[SerializeField]
public class Run : States
{
    //Usamos el constructor y le pasamos todas las variables que necesita
    public Run() : base()
    {
        //Se le pasa el nombre del estado que tiene que hacer
        stateName = STATE.RUN;
    }

    //ENTER
    public override void Enter()
    {
        //Hacemos la animación de Idle
        // anim.SetTrigger("isWalking");
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
    public override void Update()
    {
        //primero se mueve, despues hace las condiciones
        BasicCharacterStateMachine.instance.MovementInput(BasicCharacterStateMachine.instance.moveSpeed);
        BasicCharacterStateMachine.instance.Grounded();
        BasicCharacterStateMachine.instance.rb.velocity = BasicCharacterStateMachine.instance.moveDirection;

        if (BasicCharacterStateMachine.instance.moveDirection == Vector3.zero)
        {
            //Si NO se mueve pasamos a idle
            BasicCharacterStateMachine.instance.TransitionToState(new Idle());
        }
        else if (BasicCharacterStateMachine.instance.pickingUp == true)
        {
            //Si esta cerca de algo y pulsamos el boton pasamos a recogerlo
        }
        else if ((Input.GetButton("Sneak")) && (BasicCharacterStateMachine.instance.isGrounded == true))
        {
            //Si snekea pos que sneakee
            BasicCharacterStateMachine.instance.TransitionToState(new Sneak());
        }
        else if (BasicCharacterStateMachine.instance.isGrounded == false)
        {
            //Si no detecta suelo entonces se cae
            BasicCharacterStateMachine.instance.TransitionToState(new Fall());
        }

    }

    //EXIT 
    public override void Exit()
    {
        //Para evitar errores de animación
        // anim.ResetTrigger("isIdle");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

[SerializeField]
public class Fall : States
{
    public float t;
    //Usamos el constructor y le pasamos todas las variables que necesita
    public Fall() : base()
    {
        //Se le pasa el nombre del estado que tiene que hacer
        stateName = STATE.FALL;
    }

    //ENTER
    public override void Enter()
    {
        t = 0;
        //Hacemos la animación de Idle
        // anim.SetTrigger("isFalling");
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
    public override void Update()
    {
        BasicCharacterStateMachine.instance.MovementInput(BasicCharacterStateMachine.instance.moveSpeed);
        BasicCharacterStateMachine.instance.Grounded();
        BasicCharacterStateMachine.instance.GravityApply();
        BasicCharacterStateMachine.instance.rb.velocity = BasicCharacterStateMachine.instance.moveDirection;

        BasicCharacterStateMachine.instance.rb.velocity = BasicCharacterStateMachine.instance.moveDirection;
        t += Time.deltaTime;
        if (t > 0.25f)
        {
            BasicCharacterStateMachine.instance.Grounded();
            if (BasicCharacterStateMachine.instance.isGrounded == true)
            {
                //cuando termina de caer y toca suelo pasa a idle y de idle a donde sea
                BasicCharacterStateMachine.instance.TransitionToState(new Idle());
            }
        }
    }

    //EXIT 
    public override void Exit()
    {
        //Para evitar errores de animación
        // anim.ResetTrigger("isIdle");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

[SerializeField]
public class Sneak : States
{
    //Usamos el constructor y le pasamos todas las variables que necesita
    public Sneak() : base()
    {
        //Se le pasa el nombre del estado que tiene que hacer
        stateName = STATE.SNEAK;
    }

    //ENTER
    public override void Enter()
    {
        //Hacemos la animación de Idle
        // anim.SetTrigger("isSNEAKing");
        BasicCharacterStateMachine.instance.Sneak();
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
    public override void Update()
    {
        //se puede mover mientras sneakea
        BasicCharacterStateMachine.instance.MovementInput(BasicCharacterStateMachine.instance.sneakMoveSpeed);
        BasicCharacterStateMachine.instance.Grounded();
        BasicCharacterStateMachine.instance.GravityApply();
        BasicCharacterStateMachine.instance.rb.velocity = BasicCharacterStateMachine.instance.moveDirection;

        if (Input.GetButton("Sneak"))
        {
            if (BasicCharacterStateMachine.instance.isGrounded == false)
            {
                BasicCharacterStateMachine.instance.ScaleBackToNormal();
                //Si no detecta suelo entonces se cae
                BasicCharacterStateMachine.instance.TransitionToState(new Fall());
            }
            else if (BasicCharacterStateMachine.instance.hiding == true)
            {
                BasicCharacterStateMachine.instance.ScaleBackToNormal();
                //Si esta cerca de un escondite, que pase a esconderse
                // BasicCharacterStateMachine.instance.TransitionToState(new PickUp());
            }
            else if (BasicCharacterStateMachine.instance.pickingUp == true)
            {
                BasicCharacterStateMachine.instance.ScaleBackToNormal();
                //Si esta cerca de algo y pulsamos el boton pasamos a recogerlo
                // BasicCharacterStateMachine.instance.TransitionToState(new PickUp());
            }
            else if (BasicCharacterStateMachine.instance.interacting == true)
            {
                BasicCharacterStateMachine.instance.ScaleBackToNormal();
                //Si esta cerca de algo interactuable, pasamos a interactuar
                // BasicCharacterStateMachine.instance.TransitionToState(new PickUp());
            }
        }
        else
        {
            BasicCharacterStateMachine.instance.ScaleBackToNormal();
            BasicCharacterStateMachine.instance.TransitionToState(new Idle());
        }
    }

    //EXIT 
    public override void Exit()
    {
        //Para evitar errores de animación
        // anim.ResetTrigger("isIdle");        
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

[SerializeField]
public class Hide : States
{
    //Usamos el constructor y le pasamos todas las variables que necesita
    public Hide() : base()
    {
        //Se le pasa el nombre del estado que tiene que hacer
        stateName = STATE.HIDE;
    }

    //ENTER
    public override void Enter()
    {
        BasicCharacterStateMachine.instance.Sneak();
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
    public override void Update()
    {
        BasicCharacterStateMachine.instance.MovementInput(BasicCharacterStateMachine.instance.moveSpeed);
        BasicCharacterStateMachine.instance.Grounded();
        BasicCharacterStateMachine.instance.rb.velocity = BasicCharacterStateMachine.instance.moveDirection;

        if ((Input.GetButton("Sneak")))
        {
            //Si se sale del escondite vuelve a idle y a escala normal
            BasicCharacterStateMachine.instance.ScaleBackToNormal();
            BasicCharacterStateMachine.instance.TransitionToState(new Idle());
        }

    }

    //EXIT 
    public override void Exit()
    {
        //Para evitar errores de animación
        // anim.ResetTrigger("isIdle");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

// [SerializeField]
// public class PickUp : States
// {
//     //Usamos el constructor y le pasamos todas las variables que necesita
//     public PickUp() : base()
//     {
//         //Se le pasa el nombre del estado que tiene que hacer
//         stateName = STATE.PICKUP;
//     }

//     public override void Enter()
//     {
//         // Debug.Log("entra");
//         //Hacemos la animación de Idle
//         // anim.SetTrigger("isIdle");
//         //Llamamos al método Enter de la clase State
//         base.Enter();
//     }

//     //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
//     public override void Update()
//     {
//         if (BasicCharacterStateMachine.instance.pickingUp == false)
//         {

//         }
//         // Debug.Log("update");
//         BasicCharacterStateMachine.instance.MovementInput();
//         BasicCharacterStateMachine.instance.Grounded();
//         BasicCharacterStateMachine.instance.PickUp();
//         if (BasicCharacterStateMachine.instance.moveDirection != Vector3.zero)
//         {
//             // Debug.Log("deberia entrar en run");
//             //Si se mueve pasamos a correr
//             BasicCharacterStateMachine.instance.TransitionToState(new Run());
//         }        
//         else if ((Input.GetButton("Sneak")) && (BasicCharacterStateMachine.instance.isGrounded == true))
//         {
//             BasicCharacterStateMachine.instance.TransitionToState(new Sneak());
//             //Si salta pasamos al estado de salto
//         }
//         else if (BasicCharacterStateMachine.instance.isGrounded == false)
//         {
//             //Si no detecta suelo entonces se cae
//             BasicCharacterStateMachine.instance.TransitionToState(new Fall());
//         }

//         base.Update();

//     }

//     //EXIT 
//     public override void Exit()
//     {
//         //Para evitar errores de animación
//         // anim.ResetTrigger("isIdle");
//         //Llamamos al método Exit de la clase State
//         base.Exit();
//     }
// }

// [SerializeField]
// public class Interact : States
// {
//     //Usamos el constructor y le pasamos todas las variables que necesita
//     public PickUp() : base()
//     {
//         //Se le pasa el nombre del estado que tiene que hacer
//         stateName = STATE.PICKUP;
//     }

//     //ENTER
//     public override void Enter()
//     {
//         //Hacemos la animación de Idle
//         // anim.SetTrigger("isPicking");
//         Debug.Log("pickUp");
//         //Llamamos al método Enter de la clase State
//         base.Enter();
//     }

//     //UPDATE (IMPORTANTE: recordar dar salida al siguiente estado que pueda hacer)
//     public override void Update()
//     {
//         //Cuando termina de recoger la moneda pasa a idle y de idle a donde sea
//         BasicCharacterStateMachine.instance.TransitionToState(new Idle());
//         BasicCharacterStateMachine.instance.pickingUp = false;
//     }

//     //EXIT 
//     public override void Exit()
//     {
//         //Para evitar errores de animación
//         // anim.ResetTrigger("isIdle");
//         //Llamamos al método Exit de la clase State
//         base.Exit();
//     }
// }