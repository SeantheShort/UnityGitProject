using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Game Object References
    public Transform player;
    private Vector3 targetPosition;
    public float lerpSpeed;
    public Camera cam;

    void Start()
    {
        cam.orthographicSize = 400;
    }

    void FixedUpdate()
    {
        // Camera view Transition
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7, 5 * Time.deltaTime);
        
        // Lerping Camera to Object
        targetPosition = new Vector3(player.position.x, Mathf.Max(player.position.y, 0), player.position.z - 5);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
