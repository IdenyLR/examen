
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JugadorV1 : MonoBehaviour
{
    // Velocidad del jugador
    public float velocidad = 5f;

    // Controles de este jugador
    public InputActionReference controles;

    // Pico del otro jugador
    public string picoEnemigo;

    Rigidbody2D rb;

    void Start()
    {
        // Obtenemos el Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Activamos los controles
        if (controles != null)
            controles.action.Enable();
    }

    void OnDisable()
    {
        // Desactivamos los controles
        if (controles != null)
            controles.action.Disable();
    }

    void FixedUpdate()
    {
        // Leemos las teclas de este jugador
        Vector2 movimiento = controles.action.ReadValue<Vector2>();

        // Evitamos que se mueva en diagonal
        if (Mathf.Abs(movimiento.x) >= Mathf.Abs(movimiento.y))
            movimiento.y = 0;
        else
            movimiento.x = 0;

        // Movemos al jugador
        rb.linearVelocity = movimiento.normalized * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        // Si nos toca el pico contrario
        if (otro.CompareTag(picoEnemigo))
        {
            Reiniciar();
        }
    }

    void Reiniciar()
    {
        // Reiniciamos toda la escena
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
