using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasHandler : MonoBehaviour
{
    public GameManager managerScript;

    [Header("Texts etc.")]

    public TextMeshProUGUI pairsText;
    public TextMeshProUGUI timerText;

    void Update()
    {
        pairsText.text = "" + managerScript.pairsCount + "/" + managerScript.pairsRequirement;

        float minutes = Mathf.Floor(managerScript.secondsLeft / 60);
        float seconds = managerScript.secondsLeft - (minutes * 60);

        timerText.text = "" + minutes + ":" + seconds;
    }
}
