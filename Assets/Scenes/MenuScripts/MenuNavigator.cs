using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField]
    Transform storytime;

    [SerializeField]
    Text text1;
    [SerializeField]
    Text text2;

    [SerializeField]
    Button cursedButton;

    string story1;
    string story2;

    int iteration;

    bool canContinue = false;

    private void Start()
    {
        story1 = text1.text;
        story2 = text2.text;

        text1.text = "";
        text2.text = "";
    }

    private void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1")))
        {
            if (canContinue)
            {
                iteration++;
                canContinue = false;
                if (iteration == 1)
                {
                    StartCoroutine(Typewriter(story2));
                }
                else if (iteration == 2)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    public void StartButtonClicked()
    {
        cursedButton.interactable = false;
        storytime.gameObject.SetActive(true);

        StartCoroutine(Typewriter(story1));
    }

    IEnumerator Typewriter(string story)
    {
        canContinue = false;
        text1.text = "";

        foreach (char c in story)
        {
            text1.text += c;
            if (text1.text.Length == story.Length)
            {
                canContinue = true;
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
