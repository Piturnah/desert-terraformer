using System.Collections;
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

        // Determine the required state and position
        float perlinValue = TerrainManager.GetPerlinNoiseAtPoint(
            Mathf.Round((transform.position.x + GameController.worldPos.x) / 2),
            Mathf.Round((transform.position.y + GameController.worldPos.y) / 2)
        );

        /*float perlinValue = TerrainManager.GetPerlinNoiseAtPoint(
            (GameController.worldPos.x - (GameController.worldPos.x) * GameController.zoomLevel) / 2 +
            Mathf.Round(
                (
                    (transform.position.x + GameController.worldPos.x - 90)
                    * GameController.zoomLevel
                )
                / 2 / GameController.zoomLevel
            ) * GameController.zoomLevel,

            (GameController.worldPos.y - (GameController.worldPos.y) * GameController.zoomLevel) / 2 +
            Mathf.Round(
                (
                    (transform.position.y + GameController.worldPos.y - 90)
                    * GameController.zoomLevel
                )
                / 2 / GameController.zoomLevel
            ) * GameController.zoomLevel
        );
        */

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
