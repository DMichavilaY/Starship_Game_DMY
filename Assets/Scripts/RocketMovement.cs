using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public GameObject rocketPrefab;
    public float rocketSpeed = 5f;
    public float rocketLifetime = 5f;
    public float rocketDistance = 5f;
    public AudioClip shootSound; // Agrega el AudioClip desde el inspector

    private AudioSource audioSource;
    private bool isGamePaused = false;

    private Vector3 rocketOffset;
    private GameObject rocket;

    public AudioClip musicaFondo; // Agrega el AudioClip desde el inspector

    private AudioSource sountrackSource;
    private AudioLowPassFilter lowPassFilter;
    public GameObject pauseMessage; // Objeto que muestra el mensaje de pausa

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
        lowPassFilter = GetComponent<AudioLowPassFilter>(); // Obtener el componente AudioLowPassFilter

        // Desactivar el mensaje de pausa al inicio del juego
        if (pauseMessage != null)
        {
            pauseMessage.SetActive(false);
        }

        if (audioSource != null && musicaFondo != null)
        {
            sountrackSource = gameObject.AddComponent<AudioSource>(); // Añadir un nuevo AudioSource para la música de fondo
            sountrackSource.clip = musicaFondo; // Establecer el AudioClip
            sountrackSource.loop = true; // Repetir la música en bucle
            sountrackSource.Play(); // Reproducir la música al iniciar la escena
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;

            if (isGamePaused)
            {
                PausarJuego();
            }
            else
            {
                ContinuarJuego();
            }
        }

        if (!isGamePaused && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchRocket();
            PlayShootSound();
        }
    }

    void LaunchRocket()
    {
        // Obtener la posición de la punta de la nave y ajustar con rocketOffset
        Vector3 spawnPosition = transform.position + transform.forward * rocketDistance + rocketOffset;
        Quaternion spawnRotation = transform.rotation;

        // Instanciar el cohete
        rocket = Instantiate(rocketPrefab, spawnPosition, spawnRotation);

        // Obtener la dirección hacia la cual se moverá el cohete
        Vector3 rocketDirection = rocket.transform.right;

        // Iniciar el movimiento del cohete
        StartCoroutine(MoveRocket(rocket, rocketDirection));
    }

    IEnumerator MoveRocket(GameObject rocketObj, Vector3 direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < rocketLifetime)
        {
            if (!isGamePaused && rocketObj != null)
            {
                rocketObj.transform.position += direction * rocketSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
            }

            yield return null;
        }

        if (rocketObj != null)
        {
            Destroy(rocketObj);
        }
    }

    void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound); // Reproducir el sonido del disparo
        }
    }

    void PausarJuego()
    {
        Time.timeScale = 0f;
        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = true; // Habilitar el filtro LowPass al pausar el juego
        }

        // Mostrar el mensaje de pausa
        if (pauseMessage != null)
        {
            pauseMessage.SetActive(true);
        }
    }

    void ContinuarJuego()
    {
        Time.timeScale = 1f;
        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = false; // Deshabilitar el filtro LowPass al reanudar el juego
        }

        // Ocultar el mensaje de pausa
        if (pauseMessage != null)
        {
            pauseMessage.SetActive(false);
        }
    }
}
