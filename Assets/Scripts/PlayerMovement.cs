using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (horizontalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90 * Mathf.Sign(verticalInput));
        }
    }
}