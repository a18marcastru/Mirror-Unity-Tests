using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Solo el jugador local puede controlar su propio movimiento
        if (!isLocalPlayer) return;

        // Captura la entrada del jugador
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        float currentSpeed = moveSpeed;

        // Si el jugador mantiene presionado Left Shift, se activa el sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        rb.MovePosition(rb.position + currentSpeed * Time.fixedDeltaTime * movement);
    }
}
