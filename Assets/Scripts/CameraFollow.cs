using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; // Don't want Camera in the Player (aka same coo)

    // Updates the camera's position every frame as it follows Player.
    void Update()
    {
        transform.position = target.position + offset;
    }
}
