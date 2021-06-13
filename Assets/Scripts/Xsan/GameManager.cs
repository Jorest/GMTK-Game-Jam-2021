using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pairsCount; //score

    public int pairsRequirement;
    public float secondsLeft;

    private IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            secondsLeft -= 1;
        }
    }

    void Start()
    {
        StartCoroutine(CountTime());
    }
}
