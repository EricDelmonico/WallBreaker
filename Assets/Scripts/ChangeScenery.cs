﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScenery : MonoBehaviour
{
    public Color fogColor;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogColor = fogColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}