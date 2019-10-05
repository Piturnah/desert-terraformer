using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] drockTiles;
    [SerializeField]
    Sprite[] grassTiles;
    [SerializeField]
    Sprite[] rockTiles;
    [SerializeField]
    Sprite[] sandTiles;
    [SerializeField]
    Sprite[] waterTiles;

    float perlinScale = 5;
    int perlinOffsetX;
    int perlinOffsetY;

    [SerializeField]
    GameObject tileObj;

    float rockRatio = .3f;
    Vector2 mapSize = new Vector2(90, 90);

    private void Start()
    {
        perlinOffsetX = Random.Range(-300, 300);
        perlinOffsetY = Random.Range(-300, 300);

        GameController.updateTerrain += UpdateTerrain;

        GenerateTerrain();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.water = Random.Range(1, 5000);
        }
    }

    void GenerateTerrain()
    {
        for (int y = 0; y < mapSize.y; y+= 1)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Sprite[] spritePool;

                bool spawningRock = Mathf.PerlinNoise((float)x / mapSize.x * perlinScale + perlinOffsetX, (float)y / mapSize.y * perlinScale + perlinOffsetY) <= rockRatio;
                if (spawningRock)
                {
                    spritePool = drockTiles;
                }
                else
                {
                    spritePool = sandTiles;
                }
                int tileIndex = Random.Range(0, spritePool.Length - 1);

                for (int i = 0; i < 1; i ++)
                {
                    GameObject newTile = Instantiate(tileObj, new Vector2(x * 2, y * 2), Quaternion.identity);
                    SpriteRenderer tileSprite = newTile.GetComponent<SpriteRenderer>();
                    tileSprite.sprite = spritePool[(tileIndex + i) % spritePool.Length];

                    newTile.transform.parent = transform;
                }
            }
        }
    }

    void UpdateTerrain()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        float waterRatio = (GameController.water + GameController.startBoost) / GameController.waterCap;
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Sprite[] spritePool;
                float perlinValue = Mathf.PerlinNoise((float)x / mapSize.x * perlinScale + perlinOffsetX, (float)y / mapSize.y * perlinScale + perlinOffsetY);

                bool spawningRock = perlinValue <= rockRatio;
                if (spawningRock)
                {
                    if (perlinValue >= 1 - (waterRatio))
                    {
                        spritePool = rockTiles;
                    }
                    else
                    {
                        spritePool = drockTiles;
                    }
                }
                else
                {
                    if (perlinValue >= 1 - (waterRatio))
                    {
                        if (perlinValue >= 1 - (waterRatio / 2))
                        {
                            spritePool = waterTiles;
                        }
                        else
                        {
                            spritePool = grassTiles;
                        }
                    }
                    else
                    {
                        spritePool = sandTiles;
                    }
                }
                int tileIndex = Random.Range(0, spritePool.Length - 1);

                for (int i = 0; i < 1; i++)
                {
                    GameObject newTile = Instantiate(tileObj, new Vector2(x * 2, y * 2), Quaternion.identity);
                    SpriteRenderer tileSprite = newTile.GetComponent<SpriteRenderer>();
                    tileSprite.sprite = spritePool[(tileIndex + i) % spritePool.Length];

                    newTile.transform.parent = transform;
                }
            }
        }
    }
}
