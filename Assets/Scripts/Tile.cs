﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string state;
    TerrainManager parentManager;

    private void Start()
    {
        parentManager = transform.parent.GetComponent<TerrainManager>();

        GameController.updateTerrain += CheckCurrentState;    
    }

    void CheckCurrentState()
    {
        float waterRatio = (float)GameController.waterRatio;

        // Determine the required state
        float perlinValue = TerrainManager.GetPerlinNoiseAtPoint(transform.position.x / 2, transform.position.y / 2);
        string newState;
        bool spawningRock = perlinValue <= parentManager.rockRatio;
        if (spawningRock)
        {
            if (perlinValue >= 1 - (waterRatio))
            {
                newState = "rock";
            }
            else
            {
                newState = "drock";
            }
        }
        else
        {
            if (perlinValue >= 1 - (waterRatio))
            {
                if (perlinValue >= 1 - (waterRatio / 2))
                {
                    newState = "water";
                }
                else
                {
                    newState = "grass";
                }
            }
            else
            {
                newState = "sand";
            }
        }
        if (newState != state)
        {
            UpdateState(newState);
            state = newState;
        }
    }

    void UpdateState(string newState)
    {
        Sprite[] spritePool = new Sprite[0];
        switch (newState)
        {
            case "rock":
                spritePool = parentManager.rockTiles;
                break;
            case "drock":
                spritePool = parentManager.drockTiles;
                break;
            case "water":
                spritePool = parentManager.waterTiles;
                break;
            case "grass":
                spritePool = parentManager.grassTiles;
                break;
            case "sand":
                spritePool = parentManager.sandTiles;
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = spritePool[Random.Range(0, spritePool.Length)];
    }
}
