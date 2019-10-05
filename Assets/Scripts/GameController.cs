﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static float water;
    public static float waterCap = 50000;
    public static float startBoost;

    public static float waterPerSecond = 15;

    float timeBtwUpdates = 3;
    float previousUpdateTime;
    float previousUpdateRatio;
    public static event Action updateTerrain;

    private void Start()
    {
        startBoost = waterCap * 0.06f;
    }
    private void Update()
    {
        DetectInput();
        CheckWaterRatio();

        water += waterPerSecond * Time.deltaTime;
    }

    void DetectInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            water += 1;
        }
    }
    void CheckWaterRatio()
    {
        if (Time.time >= previousUpdateTime + timeBtwUpdates)
        {
            previousUpdateTime = Time.time;
            if ((water + startBoost) / waterCap > previousUpdateRatio + .02f)
            {
                previousUpdateRatio = water / waterCap;
                if (updateTerrain != null)
                {
                    updateTerrain();
                }
            }
        }
    }
}
