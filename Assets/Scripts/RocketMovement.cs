using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float rocketSpeed = 5f;
    public float destroyAfterSeconds = 5f;
    private float timer = 0f;

    private Rigidbody rb;

    // Método para configurar el movimiento inicial del cohete con una dirección específica
    public void SetInitialMovement(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * rocketSpeed;
            rb.useGravity = false; // Desactivar la gravedad
        }
    }

    void Update()
    {
        // Destruir el cohete después de un tiempo determinado
        timer += Time.deltaTime;
        if (timer >= destroyAfterSeconds)
        {
            Destroy(gameObject);
        }
    }

    // Lógica de autodestrucción al alcanzar un límite (en este ejemplo, se autodestruye al llegar a Y = 10)
    private void FixedUpdate()
    {
        if (transform.position.y >= 10f)
        {
            Destroy(gameObject);
        }
    }
}
