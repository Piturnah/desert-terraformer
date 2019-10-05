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
        if (Input.GetButtonDown("Fire1") || GameController.water > lastWaterLevel + 100)
        {
            lastWaterLevel = GameController.water;
            Instantiate(droplet, new Vector2(transform.position.x + Random.Range(-90, 90), transform.position.y), Quaternion.identity);
        }
    }
}
