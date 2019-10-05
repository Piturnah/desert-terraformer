using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Text waterText;
    [SerializeField]
    Text wpsText;

    private void Update()
    {
        waterText.text = Mathf.RoundToInt(GameController.water).ToString("00000");
        wpsText.text = "+" + GameController.waterPerSecond.ToString();
    }
}
