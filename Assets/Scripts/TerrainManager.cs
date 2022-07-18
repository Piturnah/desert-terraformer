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

    public static float perlinScale = 5;
    public static int perlinOffsetX;
    public static int perlinOffsetY;

    [SerializeField]
    GameObject tileObj;

    public static int octaves = 5;
    public static float lacunarity = 2;
    public static float persistence = 0.4f;

    public float rockRatio = .3f;
    public static Vector2 mapSize = new Vector2(90, 90);

    public static float max_noise;

    private void Start()
    {
        perlinOffsetX = Random.Range(-300, 300);
        perlinOffsetY = Random.Range(-300, 300);

        GenerateTerrain();
    }

    public static float GetPerlinNoiseAtPoint(float x, float y)
    {
        float noiseValue = 0;

        for (int octave = 0; octave < octaves; octave++)
        {
            noiseValue
                += (Mathf.PerlinNoise(
                    x / mapSize.x * perlinScale * Mathf.Pow(lacunarity, octave) + perlinOffsetX,
                    y / mapSize.y * perlinScale * Mathf.Pow(lacunarity, octave) + perlinOffsetY
                ) - 0.5f) * Mathf.Pow(persistence, octave);
        }

        noiseValue += 0.5f;
        return noiseValue;
    }

    void GenerateTerrain()
    {
        for (int y = 0; y < mapSize.y; y+= 1)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Sprite[] spritePool;
                string newState;

                float noiseValue = GetPerlinNoiseAtPoint(x, y);
                if (noiseValue > max_noise)
                {
                    max_noise = noiseValue;
                }

                bool spawningRock = noiseValue <= rockRatio;
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
