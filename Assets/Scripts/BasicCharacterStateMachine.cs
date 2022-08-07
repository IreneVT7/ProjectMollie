using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterStateMachine : MonoBehaviour
{[Header("Movement")]
    [Tooltip("Rigidbody del personaje")]
    public Rigidbody rb;
    // [Tooltip("Animator del personaje")]
    // public Animator anim;
    [Tooltip("Velocidad a la que se mueve el personaje")]
    public float moveSpeed;
    [HideInInspector]
    public Vector3 moveDirection;
    [Tooltip("Fuerza del salto")]
    public float jumpForce;
    [Tooltip("Factor de gravedad. Se multiplica con la fuerza de gravedad de Unity")]
    public float gravityScale = 5f;
    [HideInInspector]
    [Tooltip("¿El personaje esta tocando el suelo?")]
    public bool isGrounded;
    [Tooltip("Distancia que mide el rayo de deteccion suelo")]
    public float groundCheckDistance = 1;
    [Tooltip("Offset de la deteccion suelo")]
    public float groundCheckOffset;
    [Tooltip("Capa suelo. La usa el raycast para diferenciar lo que es suelo de lo que no lo es")]
    public LayerMask groundLayers;

    public bool pickingUp = false;

    //El estado actual en el que está ese enemigo
    public States currentState;


    public void Grounded()
    {
        RaycastHit hit;
        //raycast para detectar el suelo. si lo detecta se pone verde y si no rojo. el raycast depende de la gravedad
        if (Physics.Raycast(transform.position + groundCheckOffset * Vector3.up, Vector3.down, out hit, groundCheckDistance, groundLayers))
        {
            Debug.DrawRay(transform.position + groundCheckOffset * Vector3.up, Vector3.down * groundCheckDistance, Color.green);
            //esta tocando suelo
            isGrounded = true;
            //reseteamos la y cuando esta parado en el suelo para que no haga cosas raras
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        }
        else
        {
            Debug.DrawRay(transform.position + groundCheckOffset * Vector3.up, Vector3.down * groundCheckDistance, Color.red);
            //no esta tocando suelo
            isGrounded = false;
        }
    }

    public void GravityApply()
    {
        //aplica la gravedad al personaje
        //esta multiplicada por un float (gravityScale) para regular mejor la fuerza con la que cae
        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
    }

    public void MovementInput()
    {
        //al principio guarda la y del jugador para que no se sobreescriba y se normalice junto con la x y la z. Luego la vuelve a aplicar al moveDirection y asi se mantiene
        float yStore = moveDirection.y;
        //A la direccion de movimiento se le pasa un vector resultante de la suma de su movimiento en ambos ejes.        
        var input = ((transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"))).normalized;
        moveDirection = input;

        //se multiplica por la velocidad de movimiento
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = yStore;
    }


    //SINGLETON
    public static BasicCharacterStateMachine instance;
    private void Awake()
    {
        //si no hay ninguna instancia de este script. este es el script correcto. si hay otro destruye este
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos las referencias y variables
        // anim = this.GetComponent<Animator>();
        //Elegimos el estado en el que empieza este personaje
        TransitionToState(new Idle());
    }

    // Update is called once per frame
    void Update()
    {
        //Si el evento en el que estoy es el de update, hago el método correspondiente
        currentState.Update();
        //si se pulsa el click izquierdo (fire1) entonces notifica al observer de que una coin esta siendo recogida
        if (Input.GetButtonDown("Fire1"))
        {
            
        }
    }

    public void TransitionToState(States state)
    {
        //termina el estado y pasa al enter del siguiente
        state.Exit();
        currentState = state;
        currentState.Enter();
    }

}
