using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Variables
    public Vector3 positionOne;
    public Vector3 positionTwo;
    private bool targetPos = true;
    public float speed;
    
    void Update()
    {
        if (targetPos)
            transform.position = Vector3.Lerp(transform.position, positionOne, Time.deltaTime * speed);
        else
            transform.position = Vector3.Lerp(transform.position, positionTwo, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, positionOne) < 0.1f)
            targetPos = false;
        else if (Vector3.Distance(transform.position, positionTwo) < 0.1f)
            targetPos = true;
    }
}
