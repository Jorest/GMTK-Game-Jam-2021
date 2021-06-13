using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveMe : MonoBehaviour
{
    public float lifeTimeSeconds = 20f;
    private float cooldown;

    void Start()
    {
        cooldown = lifeTimeSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0.0f)
        {
            Destroy(this.gameObject);
        }

    }
}
