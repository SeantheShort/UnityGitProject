using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    // Game Object References
    public ColorManager manager;
    public Image selfImage;

    void Start()
    {
        selfImage = GetComponent<Image>();
        manager = GameObject.FindObjectOfType<ColorManager>();

    }

    void Update()
    {
        selfImage.color = manager.universalColor;
    }
}
