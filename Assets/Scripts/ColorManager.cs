using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    // Variables
    public Color universalColor;

    // Game Object References
    [SerializeField] private Slider slider;
    private static ColorManager colorInstance;

    void Start()
    {
        Debug.Log("Color Manager Loaded");
        universalColor = Color.HSVToRGB(0.6f, 0.36f, 1f);
    }

    void Awake()
    {
        // Makes sure the object stays inbetween scenes
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (slider != null)
        {
            // Updates Color
            universalColor = Color.HSVToRGB(slider.value, 0.36f, 1f);
        }
    }
}
