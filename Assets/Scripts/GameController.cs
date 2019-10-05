using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float water;
    public static float waterCap = 5000;

    private void Awake()
    {
        Sprite[] mySprites = Resources.LoadAll<Sprite>("");
    }
}
