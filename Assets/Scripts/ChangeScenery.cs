using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScenery : MonoBehaviour
{
    public Color fogColor;
    public Light light;
    public Color lightColor;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogColor = fogColor;
        light.color = lightColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
