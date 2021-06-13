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

    private IEnumerator CountTime()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(1f);

            secondsLeft -= 1;

            if(secondsLeft == 0)
            {
                gameOn = false;
            }
        }
    }

    void Start()
    {
        StartCoroutine(CountTime());
    }

    void Update()
    {
        if (!gameOn)
        {
            //slow time
        }
    }
}
