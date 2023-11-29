using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimites : MonoBehaviour
{
    public float normalSpeed = 0f;
    public float turboSpeedMultiplier = 0f;
    private float currentSpeed;

    public float rotationSpeed = 0f; // Velocidad de rotación suave
    public AnimationCurve curve;
    [SerializeField] private ParticleSystem particulas;
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + movementVector.x, -530, 530), Mathf.Clamp(transform.position.y + movementVector.z, -350, 350), 0);

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
            particulas.Play();
        }
        else
        {
            currentSpeed = normalSpeed;
        }
    }
}