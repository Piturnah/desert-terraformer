using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Text waterText;

    private void Update()
    {
        waterText.text = GameController.water.ToString("0000");
    }
}
