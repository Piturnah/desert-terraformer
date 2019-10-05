using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static float water;
    public static float waterCap = 50000;

    float timeBtwUpdates = 3;
    float previousUpdateTime;
    float previousUpdateRatio;
    public static event Action updateTerrain;

    private void Update()
    {
        DetectInput();
        CheckWaterRatio();
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
            if (water / waterCap > previousUpdateRatio + .02f)
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
