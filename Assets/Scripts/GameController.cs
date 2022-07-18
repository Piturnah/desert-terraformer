using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public float movementSpeed;
    public static Vector2 worldPos;
    //public float zoomSpeed;
    //public static float zoomLevel = 1;

    public static float water;
    public static float waterCap = 1600;
    public static float startBoost = 1500;
    public static float waterMultiplier;

    public static float waterPerSecond = 0;

    float timeBtwUpdates = .03f;
    float previousUpdateTime;
    
    public static double waterRatio;
    public double waterInterpolationFactor;
    double previousUpdateRatio;
    public static event Action updateTerrain;

    private void Start()
    {
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

        Vector2 movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        worldPos += movementDirection * movementSpeed * Time.deltaTime; // * zoomLevel

        //zoomLevel -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        //zoomLevel = Mathf.Clamp(zoomLevel, 0.0001f, float.MaxValue);

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    zoomLevel = 1;
        //}
    }
    void CheckWaterRatio()
    {
        waterInterpolationFactor = waterPerSecond / (waterCap * 50);
        waterRatio += waterInterpolationFactor * Time.deltaTime;

        if (Time.time >= previousUpdateTime + timeBtwUpdates)
        {
            previousUpdateTime = Time.time;
            
            previousUpdateRatio = waterRatio;
            updateTerrain?.Invoke();
        }
    }
}
