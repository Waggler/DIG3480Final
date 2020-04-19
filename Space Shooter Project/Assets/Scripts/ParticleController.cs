﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem ps;
    public float scrollSpeed = 1.0F;
    
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = scrollSpeed;
    }

}