using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEnd : MonoBehaviour
{
    private Trail trailScript;

    void Start()
    {
        trailScript = GameObject.Find("Witch").transform.GetChild(1).GetComponent<Trail>();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Witch"))
        {
            if (trailScript.leaveTrail)
            {
                trailScript.TrailClosed();
            }
        }
    }
}
