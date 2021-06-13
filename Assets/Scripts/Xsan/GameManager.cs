using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int pairsCount; //score

    public int pairsRequirement;
    public float secondsLeft;

    private bool gameOn = true;
    private bool slowtime;
    public WitchMovement witchScript;
    public CanvasHandler canvasScript;

    public string[] monstersInLevel;
    public string[] monsterTypes;

    private float alpha = 0f;

    private IEnumerator WriteText(string text, bool end)
    {
        canvasScript.centerText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            canvasScript.centerText.text += text[i];

            yield return new WaitForSeconds(0.05f);
        }

        if (end)
        {
            yield return new WaitForSeconds(1f);
            canvasScript.centerText.text = "";
        }
        else
        {
            slowtime = true;
        }
    }

    private void WinOrNot()
    {
        if(pairsCount >= pairsRequirement)
        {
            StartCoroutine(WriteText("You're a great matchmaker!", false));
        }
        else
        {
            StartCoroutine(WriteText("You didn't match enough pairs :(", false));
        }
    }

    private IEnumerator CountTime()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(1f);

            secondsLeft -= 1;

            if(secondsLeft == 0)
            {
                gameOn = false;

                WinOrNot();
            }
        }
    }

    public void RandomizePair()
    {
        int id1 = Random.Range(0, monstersInLevel.Length);
        int id2 = Random.Range(0, monstersInLevel.Length);
        monsterTypes[0] = monstersInLevel[id1];
        monsterTypes[1] = monstersInLevel[id2];

        canvasScript.headOne.sprite = canvasScript.headSprites[id1];
        canvasScript.headTwo.sprite = canvasScript.headSprites[id2];
    }

    void Start()
    {
        Time.timeScale = 1f;

        monsterTypes = new string[2];

        RandomizePair();

        StartCoroutine(CountTime());

        StartCoroutine(WriteText("Spread the love!", true));
    }

    void Update()
    {
        if (slowtime)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.01f, 0.025f);

            alpha = Mathf.Lerp(alpha, 1f, 0.025f);
            canvasScript.blackScreen.color = new Color(1, 1, 1, alpha);

            if(alpha >= 0.95f)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }
}
