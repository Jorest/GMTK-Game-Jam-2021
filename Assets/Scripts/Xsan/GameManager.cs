using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pairsCount; //score

    public int pairsRequirement;
    public float secondsLeft;


    private bool gameOn = true;
    public WitchMovement witchScript;

    public string[] monsterTypes;

    private void WinOrNot()
    {
        if(pairsCount >= pairsRequirement)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Need to restart");
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

    void Start()
    {
        Time.timeScale = 1f;

        monsterTypes = new string[2];
        monsterTypes[0] = "demon";
        monsterTypes[1] = "ghost";

        StartCoroutine(CountTime());
    }

    void Update()
    {
        if (!gameOn)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.01f, 0.025f);
        }
    }
}
