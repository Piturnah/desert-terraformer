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
    Vector2 mapSize = new Vector2(90, 180);

    private void Start()
    {
        perlinOffsetX = Random.Range(-300, 300);
        perlinOffsetY = Random.Range(-300, 300);

        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        for (int y = 0; y < mapSize.y; y+= 2)
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

                for (int i = 0; i < 2; i ++)
                {
                    GameObject newTile = Instantiate(tileObj, new Vector2(x * 2, y + i), Quaternion.identity);
                    SpriteRenderer tileSprite = newTile.GetComponent<SpriteRenderer>();
                    tileSprite.sprite = spritePool[(tileIndex + i) % spritePool.Length];

                    newTile.transform.parent = transform;
                }
            }
        }
    }
}
