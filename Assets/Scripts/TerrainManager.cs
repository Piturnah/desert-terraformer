using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainManager : MonoBehaviour
{
    public Sprite[] drockTiles;
    public Sprite[] grassTiles;
    public Sprite[] rockTiles;
    public Sprite[] sandTiles;
    public Sprite[] waterTiles;

    public float perlinScale = 5;
    public int perlinOffsetX;
    public int perlinOffsetY;

    [SerializeField]
    GameObject tileObj;

    public float rockRatio = .3f;
    public Vector2 mapSize = new Vector2(90, 90);

    private void Start()
    {
        perlinOffsetX = Random.Range(-300, 300);
        perlinOffsetY = Random.Range(-300, 300);

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
                string newState;

                bool spawningRock = Mathf.PerlinNoise((float)x / mapSize.x * perlinScale + perlinOffsetX, (float)y / mapSize.y * perlinScale + perlinOffsetY) <= rockRatio;
                if (spawningRock)
                {
                    spritePool = drockTiles;
                    newState = "drock";
                }
                else
                {
                    spritePool = sandTiles;
                    newState = "sand";
                }
                int tileIndex = Random.Range(0, spritePool.Length - 1);

                for (int i = 0; i < 1; i ++)
                {
                    GameObject newTile = Instantiate(tileObj, new Vector2(x * 2, y * 2), Quaternion.identity);
                    SpriteRenderer tileSprite = newTile.GetComponent<SpriteRenderer>();
                    tileSprite.sprite = spritePool[(tileIndex + i) % spritePool.Length];

                    Tile tileScript = newTile.GetComponent<Tile>();
                    if (tileScript != null)
                    {
                        tileScript.state = newState;
                    }

                    newTile.transform.parent = transform;
                }
            }
        }
    }
}
