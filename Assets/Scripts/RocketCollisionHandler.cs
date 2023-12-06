using UnityEngine;

public class RocketCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject); // Destruir el asteroide
            Destroy(gameObject); // Destruir el cohete
        }
    }
}
