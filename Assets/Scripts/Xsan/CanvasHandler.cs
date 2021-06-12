using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasHandler : MonoBehaviour
{
    public GameManager managerScript;

    [Header("Texts etc.")]

    public TextMeshProUGUI pairsText;

    void Update()
    {
        pairsText.text = "" + managerScript.pairsCount;
    }
}
