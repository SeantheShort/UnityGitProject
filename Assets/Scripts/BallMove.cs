using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            this.transform.position += new Vector3(0, 0, 10f*Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            this.transform.position -= new Vector3(0, 0, 10f*Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            this.transform.position -= new Vector3(10f*Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.D))
            this.transform.position += new Vector3(10f*Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(new Vector3(0, 10, 0));
    }
}
