using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimites : MonoBehaviour
{
    public float normalSpeed = 0f;
    public float turboSpeedMultiplier = 0f;
    private float currentSpeed;
    public GameObject proyectilPrefab;
    public float frecuenciaDisparo = 0.5f;
    public float velocidadProyectil = 10f;
    public float distanciaDisparo = 10f;
    public float tiempoDestruccionProyectil = 5f;
    private float tiempoUltimoDisparo;


    public float rotationSpeed = 0f; // Velocidad de rotación suave
    public AnimationCurve curve;
    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
        }

        Vector3 movementVector = inputVector * currentSpeed * Time.deltaTime;

        // Movimiento de la nave
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + movementVector.x, -400, 400), Mathf.Clamp(transform.position.y + movementVector.z, -400, 400), 0);

        // Rotación de la nave gradual
        if (inputVector != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, -targetAngle); // Invertir el ángulo
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Verificar si se presiona la tecla Shift para activar el turbo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = normalSpeed * turboSpeedMultiplier;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }

    // Método para el disparo del proyectil
    private void Disparar()
    {
        if (Time.time > tiempoUltimoDisparo + frecuenciaDisparo)
        {
            tiempoUltimoDisparo = Time.time;

            // Obtiene la posición de disparo (10 unidades desde la nave en la dirección hacia adelante)
            Vector3 posicionDisparo = transform.position + transform.up * distanciaDisparo;

            // Instancia el proyectil en la posición de disparo y con la rotación de la nave
            GameObject nuevoProyectil = Instantiate(proyectilPrefab, posicionDisparo, transform.rotation);

            // Accede al Rigidbody2D del proyectil
            Rigidbody2D rbProyectil = nuevoProyectil.GetComponent<Rigidbody2D>();
            if (rbProyectil != null)
            {
                // Aplica una velocidad al proyectil en la dirección de la nave
                rbProyectil.velocity = transform.up * velocidadProyectil;
            }

            // Destruye el proyectil después de cierto tiempo
            Destroy(nuevoProyectil, tiempoDestruccionProyectil);
        }
    }


}