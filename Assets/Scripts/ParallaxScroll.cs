using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float backgroundSize = 15f;
    public Transform player;
    public float repeatThreshold = 0.8f;

    void Update()
    {
        if (Mathf.Abs(player.position.x) > backgroundSize * 0.5f * repeatThreshold)
        {
            RepositionBackgroundX();
        }

        if (Mathf.Abs(player.position.y) > backgroundSize * 0.5f * repeatThreshold)
        {
            RepositionBackgroundY();
        }
    }

    private void RepositionBackgroundX()
    {
        float newX = Mathf.Floor(player.position.x / backgroundSize) * backgroundSize;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void RepositionBackgroundY()
    {
        float newY = Mathf.Floor(player.position.y / backgroundSize) * backgroundSize;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
