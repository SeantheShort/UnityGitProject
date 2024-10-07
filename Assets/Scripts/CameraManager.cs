using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPosition;
    public float lerpSpeed;
    
    void FixedUpdate()
    {
        targetPosition = new Vector3(Mathf.Clamp(player.position.x, -3, 3), Mathf.Max(player.position.y, 0), player.position.z - 5);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
