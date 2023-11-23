using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimites : MonoBehaviour
{

    public int speed;
    public AnimationCurve curve;

    void Update()
    {

        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
        }

        Vector3 movementVector = inputVector * speed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x + movementVector.x, -23, 23), Mathf.Clamp(transform.position.y + movementVector.z, -23, 23), 0);

    }
}