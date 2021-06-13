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
    public TextMeshProUGUI centerText;
    public SpriteRenderer blackScreen;

    public Transform rotatePoint;
    public Transform head1;
    public Transform head2;
    [HideInInspector]
    public SpriteRenderer headOne;
    [HideInInspector]
    public SpriteRenderer headTwo;

    // ghost
    // demon
    // alien
    public List<Sprite> headSprites;

    void Start()
    {
        headOne = head1.GetComponent<SpriteRenderer>();
        headTwo = head2.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        pairsText.text = "" + managerScript.pairsCount + "/" + managerScript.pairsRequirement;

        float minutes = Mathf.Floor(managerScript.secondsLeft / 60);
        float seconds = managerScript.secondsLeft - (minutes * 60);

        timerText.text = "" + minutes + ":" + seconds;

        rotatePoint.Rotate(new Vector3(0, 0, 0.35f), Space.Self);
        head1.rotation = Quaternion.Euler(0, 0, 0);
        head2.rotation = Quaternion.Euler(0, 0, 0);
    }
}
