using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShutOff : MonoBehaviour
{
    ParticleSystem particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public IEnumerator ShutOff()
    {
        particles.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        yield return new WaitForSeconds(3f);

        //TODO: need to figure out if permormance heavy?
        gameObject.SetActive(false);
    }
}
