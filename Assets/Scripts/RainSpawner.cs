using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject droplet;

    float lastWaterLevel;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            lastWaterLevel = GameController.water;
            Instantiate(droplet, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward, Quaternion.identity);
        }
    }
}
