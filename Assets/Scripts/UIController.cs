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
        waterText.text = Mathf.RoundToInt(GameController.water).ToString("00,000");

        string wps = (GameController.waterPerSecond < 1000) ? GameController.waterPerSecond.ToString() : (GameController.waterPerSecond / 1000).ToString("F2") + "K";
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
            string num = (values[0] < 1000) ? (values[0] * values[2]).ToString() : ((float)values[0] / 1000).ToString("F1") + "K";
            newPurchaseTransform.Find("Current bonus").GetComponent<Text>().text = "+" + num + " WPS";

            int numInt = (Mathf.RoundToInt(Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]))));
            num = (numInt < 1000) ? (Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]))).ToString() :
                ((float)(Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]))) / 1000).ToString("F1") + "K";
            newPurchaseTransform.Find("Button").Find("Price").GetComponent<Text>().text = num;
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
                newPurchaseTransform.Find("Bonus").GetComponent<Text>().text = "+" + ((values[0]<1000)? values[0].ToString(): ((float)values[0]/1000).ToString("F2")+"K");

                string num = (values[0] < 1000) ? (values[0] * values[2]).ToString() : ((float)values[0] / 1000).ToString("F2") + "K";
                newPurchaseTransform.Find("Current bonus").GetComponent<Text>().text = "+" + num + " WPS";

                int numInt = (Mathf.RoundToInt(Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]))));
                num = (numInt < 1000) ? (Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]))).ToString() :
                    ((float)(Mathf.RoundToInt(values[1] * Mathf.Pow(multiplier, values[2]))) / 1000).ToString("F2") + "K";
                newPurchaseTransform.Find("Button").Find("Price").GetComponent<Text>().text = num;

                newPurchaseTransform.gameObject.SetActive(false);
            }
        }
    }
}

public static class ItemInfo
{
    public static int[] bucket = { 1, 30, 0 };
    public static int[] raincloud = { 3, 90, 0 };
    public static int[] fremen = { 6, 130, 0 };
    public static int[] windtrap = { 15, 700, 0 };
    public static int[] aqueducts = { 40, 3000, 0 };
    public static int[] stormcloud = { 120, 10000, 0 };
    public static int[] sandworm = { 300, 20000, 0 };
}
