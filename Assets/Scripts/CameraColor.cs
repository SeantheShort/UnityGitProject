using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour
{
    public ColorManager manager;

    private void Start()
    {
        manager = GameObject.FindAnyObjectByType<ColorManager>();
    }
    void Update()
    {
        GetComponent<Camera>().backgroundColor = manager.universalColor;
    }
}
