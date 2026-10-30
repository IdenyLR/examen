
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5f;

    public InputActionReference controles;

    public string picoEnemigo;

    Rigidbody2D rb;
    CircleCollider2D cuerpo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cuerpo = GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
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
        Vector2 movimiento = controles.action.ReadValue<Vector2>();

        if (Mathf.Abs(movimiento.x) >= Mathf.Abs(movimiento.y))
            movimiento.y = 0;
        else
            movimiento.x = 0;

        rb.linearVelocity = movimiento.normalized * velocidad;

        if (movimiento != Vector2.zero)
        {
            float angulo = Mathf.Atan2(
                movimiento.y,
                movimiento.x
            ) * Mathf.Rad2Deg;

            rb.MoveRotation(angulo);
    }

    Collider2D[] contactos = new Collider2D[10];
    int cantidad = cuerpo.GetContacts(contactos);

    for(int i = 0; i<cantidad; i++)
    {
        if (contactos[i].CompareTag(picoEnemigo))
        {
            Reiniciar();
            return;
}
}
}

    void Reiniciar()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
