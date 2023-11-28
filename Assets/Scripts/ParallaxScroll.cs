using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 parallaxEffectMultiplier = new Vector2(0.5f, 0.5f);
    public Transform cam;
    private Vector3 previousCamPos;

    private void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    private void Update()
    {
        Vector2 parallax = (previousCamPos - cam.position) * parallaxEffectMultiplier;
        Vector3 backgroundTargetPos = new Vector3(transform.position.x + parallax.x, transform.position.y + parallax.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, Time.deltaTime * 5f);

        previousCamPos = cam.position;
    }
}