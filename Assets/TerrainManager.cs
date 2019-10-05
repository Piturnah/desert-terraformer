using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] terrainTiles;
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

    [SerializeField]
    GameObject tileObj;

    float rockRatio = .2f;
    Vector2 mapSize = new Vector2(30, 60);

    private void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        for (int y = 0; y < mapSize.y; y+= 2)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Sprite[] spritePool;

                bool spawningRock = Random.Range(0f, 1f) <= rockRatio;
                if (spawningRock)
                {
                    spritePool = drockTiles;
                }
                else
                {
                    spritePool = sandTiles;
                }
                int tileIndex = Random.Range(0, spritePool.Length - 1);

                GameObject bottomTile = Instantiate(tileObj, new Vector2(x * 2, y), Quaternion.identity);
                SpriteRenderer bottomTileSprite = bottomTile.GetComponent<SpriteRenderer>();
                bottomTileSprite.sprite = spritePool[(tileIndex + 1) % spritePool.Length];

                GameObject topTile = Instantiate(tileObj, new Vector2(x * 2, y + 1), Quaternion.identity);
                SpriteRenderer topTileSprite = topTile.GetComponent<SpriteRenderer>();
                topTileSprite.sprite = spritePool[tileIndex % spritePool.Length];
            }
        }
    }
}
