using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIterator : MonoBehaviour
{
    int iteration = -1;
    float baseWaterCap = 50000;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        newIteration(0f);
    }

    void newIteration(float oldWater)
    {
        iteration++;

        //GameController.waterCap = baseWaterCap * Mathf.Pow(2f, iteration);
        //GameController.startBoost = GameController.waterCap * .03f;

        GameController.waterMultiplier = 1 + (oldWater / 5000 * 0.4f);
    }
}
