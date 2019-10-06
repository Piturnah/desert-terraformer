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

    [SerializeField]
    Transform scrollContent;

    float multiplier = 1.07f;

    private void Start()
    {
        SetDisplayValues();
    }
    private void Update()
    {
        waterText.text = Mathf.RoundToInt(GameController.water).ToString("00000");
        wpsText.text = "+" + GameController.waterPerSecond.ToString();

        foreach (Transform child in scrollContent)
        {
            int[] values = new int[2];
            Transform newPurchaseTransform = null;

            switch (child.GetSiblingIndex())
            {
                case 0:
                    values = ItemInfo.bucket;
                    newPurchaseTransform = scrollContent.transform.Find("Courier");
                    break;
                case 1:
                    values = ItemInfo.raincloud;
                    newPurchaseTransform = scrollContent.transform.Find("Raincloud");
                    break;
                case 2:
                    values = ItemInfo.fremen;
                    newPurchaseTransform = scrollContent.transform.Find("Fremen");
                    break;
                case 3:
                    values = ItemInfo.windtrap;
                    newPurchaseTransform = scrollContent.transform.Find("Windtrap");
                    break;
                case 4:
                    values = ItemInfo.aqueducts;
                    newPurchaseTransform = scrollContent.transform.Find("Aqueducts");
                    break;
                case 5:
                    values = ItemInfo.stormcloud;
                    newPurchaseTransform = scrollContent.transform.Find("Stormcloud");
                    break;
                case 6:
                    values = ItemInfo.sandworm;
                    newPurchaseTransform = scrollContent.transform.Find("Sandworm");
                    break;

            }

            if (Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2])) > GameController.water)
            {
                child.Find("Button").gameObject.GetComponent<Button>().interactable = false;
            } else if (child.gameObject.activeInHierarchy == false)
            {
                child.gameObject.SetActive(true);
            } else if (!child.Find("Button").gameObject.GetComponent<Button>().IsInteractable())
            {
                child.Find("Button").gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void PurchaseItem(string item)
    {
        int[] values = new int[2];
        Transform newPurchaseTransform = null;

        switch (item)
        {
            case "bucket":
                values = ItemInfo.bucket;
                newPurchaseTransform = scrollContent.transform.Find("Courier");
                break;
            case "raincloud":
                values = ItemInfo.raincloud;
                newPurchaseTransform = scrollContent.transform.Find("Raincloud");
                break;
            case "fremen":
                values = ItemInfo.fremen;
                newPurchaseTransform = scrollContent.transform.Find("Fremen");
                break;
            case "windtrap":
                values = ItemInfo.windtrap;
                newPurchaseTransform = scrollContent.transform.Find("Windtrap");
                break;
            case "aqueducts":
                values = ItemInfo.aqueducts;
                newPurchaseTransform = scrollContent.transform.Find("Aqueducts");
                break;
            case "stormcloud":
                values = ItemInfo.stormcloud;
                newPurchaseTransform = scrollContent.transform.Find("Stormcloud");
                break;
            case "sandworm":
                values = ItemInfo.sandworm;
                newPurchaseTransform = scrollContent.transform.Find("Sandworm");
                break;
        }
        int price = Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]));
        GameController.water -= price;
        GameController.waterPerSecond -= values[0] * values[2];
        values[2]++;
        GameController.waterPerSecond += values[0] * values[2];

        // Update the displayed values

        if (newPurchaseTransform != null)
        {
            newPurchaseTransform.Find("Current bonus").GetComponent<Text>().text = "+" + (values[0] * values[2]).ToString() + " WPS";
            newPurchaseTransform.Find("Button").Find("Price").GetComponent<Text>().text = (Mathf.RoundToInt(Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2])))).ToString();
        }
    }
    void SetDisplayValues()
    {
        foreach (Transform child in scrollContent)
        {
            int[] values = new int[2];
            Transform newPurchaseTransform = null;

            switch (child.GetSiblingIndex())
            {
                case 0:
                    values = ItemInfo.bucket;
                    newPurchaseTransform = scrollContent.transform.Find("Courier");
                    break;
                case 1:
                    values = ItemInfo.raincloud;
                    newPurchaseTransform = scrollContent.transform.Find("Raincloud");
                    break;
                case 2:
                    values = ItemInfo.fremen;
                    newPurchaseTransform = scrollContent.transform.Find("Fremen");
                    break;
                case 3:
                    values = ItemInfo.windtrap;
                    newPurchaseTransform = scrollContent.transform.Find("Windtrap");
                    break;
                case 4:
                    values = ItemInfo.aqueducts;
                    newPurchaseTransform = scrollContent.transform.Find("Aqueducts");
                    break;
                case 5:
                    values = ItemInfo.stormcloud;
                    newPurchaseTransform = scrollContent.transform.Find("Stormcloud");
                    break;
                case 6:
                    values = ItemInfo.sandworm;
                    newPurchaseTransform = scrollContent.transform.Find("Sandworm");
                    break;

            }

            if (newPurchaseTransform != null)
            {
                newPurchaseTransform.Find("Bonus").GetComponent<Text>().text = "+" + values[0].ToString();
                newPurchaseTransform.Find("Current bonus").GetComponent<Text>().text = "+" + (values[0] * values[2]).ToString() + " WPS";
                newPurchaseTransform.Find("Button").Find("Price").GetComponent<Text>().text = (Mathf.RoundToInt(Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2])))).ToString();

                newPurchaseTransform.gameObject.SetActive(false);
            }
        }
    }
}

public static class ItemInfo
{
    public static int[] bucket = { 1, 30, 0 };
    public static int[] raincloud = { 3, 70, 0 };
    public static int[] fremen = { 6, 130, 0 };
    public static int[] windtrap = { 15, 310, 0 };
    public static int[] aqueducts = { 40, 810, 0 };
    public static int[] stormcloud = { 120, 2300, 0 };
    public static int[] sandworm = { 300, 10000, 0 };
}
