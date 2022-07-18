using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static float water;
    public static float waterCap = 1600;
    public static float startBoost = 1500;
    public static float waterMultiplier;

    public static float waterPerSecond = 0;

    float timeBtwUpdates = .1f;
    float previousUpdateTime;
    
    public static double waterRatio;
    public double waterInterpolationFactor;
    double previousUpdateRatio;
    public static event Action updateTerrain;

    private void Start()
    {
        waterRatio = Mathf.Clamp01(1 - TerrainManager.max_noise - 0.001f);// - 0.00001;

        startBoost = waterCap * 0.06f;

        water = 0;
        waterPerSecond = 0;
    }
    private void Update()
    {
        DetectInput();
        CheckWaterRatio();

        float waterPerSecondFixed = waterPerSecond * waterMultiplier;
        water += waterPerSecondFixed * Time.deltaTime;
    }

    void DetectInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            water += 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            waterPerSecond += 20;
        }
    }
    void CheckWaterRatio()
    {
        waterInterpolationFactor = waterPerSecond / (waterCap * 50);
        waterRatio += waterInterpolationFactor * Time.deltaTime;
        Debug.Log(waterRatio);

        if (Time.time >= previousUpdateTime + timeBtwUpdates)
        {
            previousUpdateTime = Time.time;
            
            if (waterRatio > previousUpdateRatio + .005f)
            {
                previousUpdateRatio = waterRatio;
                updateTerrain?.Invoke();
            }
        }
    }
}
