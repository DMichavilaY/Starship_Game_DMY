using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform target; // Referencia al objeto que los asteroides seguirán (la nave en este caso)
    public float spawnRate = 2f; // Frecuencia de generación de asteroides
    public float asteroidMinSize = 0.5f; // Tamaño mínimo del asteroide
    public float asteroidMaxSize = 2.0f; // Tamaño máximo del asteroide
    public float asteroidSpeed = 3f; // Velocidad a la que se moverán los asteroides

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            // Generar una posición aleatoria fuera del rango de visión de la cámara
            Vector3 spawnPosition = GetRandomOffscreenPosition();

            // Generar un tamaño aleatorio para el asteroide
            float asteroidSize = Random.Range(asteroidMinSize, asteroidMaxSize);

            // Instanciar el asteroide en la posición aleatoria
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            asteroid.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);

            // Obtener la dirección hacia la nave
            Vector3 direction = Vector3.zero;
            if (target != null)
            {
                direction = (target.position - asteroid.transform.position).normalized;
            }

            // Iniciar el movimiento del asteroide hacia la nave
            StartCoroutine(MoveAsteroid(asteroid, direction));
        }
    }

    Vector3 GetRandomOffscreenPosition()
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calcular una posición aleatoria fuera del rango de la cámara
        float randomX = Random.Range(1 * cameraWidth, 2 * cameraWidth);
        float randomY = Random.Range(1 * cameraHeight, 2 * cameraHeight);

        // Asegurarse de que la posición aleatoria sea opuesta a la posición actual de la cámara
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        // Si la posición aleatoria está en el mismo lado que la cámara, cambiar su signo
        if (randomPosition.x * cameraPosition.x > 0)
        {
            randomPosition.x *= -1;
        }
        if (randomPosition.y * cameraPosition.y > 0)
        {
            randomPosition.y *= -1;
        }

        return randomPosition;
    }

    IEnumerator MoveAsteroid(GameObject asteroid, Vector3 direction)
    {
        while (asteroid != null)
        {
            if (target != null)
            {
                direction = (target.position - asteroid.transform.position).normalized;
                asteroid.transform.position += direction * asteroidSpeed * Time.deltaTime;
            }
            else
            {
                break;
            }

            yield return null;
        }

        if (asteroid != null)
        {
            Destroy(asteroid);
        }
    }
}
