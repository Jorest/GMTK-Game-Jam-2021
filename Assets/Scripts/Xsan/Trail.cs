using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public AudioClip trailSound;
    public GameObject trailPointPrefab;
    public GameObject trailEndPrefab;
    public Transform trailGroup;

    public float distanceToNextPoint;
    //public int maxTrailLength;
    public float timeBeforeFade;
    public int trailCountUntilTrailEnd;

    public List<GameObject> trailPoints;
    public List<GameObject> oldTrailPoints;
    public List<GameObject> leftOverParticles;

    public GameObject trailDustPrefab;

    private GameObject trailEnd;

    private GameObject currentPoint;
    private GameObject previousPoint;

    //private LineRenderer currentPointRenderer;
    //private LineRenderer prevoiusPointRenderer;

    public bool leaveTrail;

    private bool playingSFX=false;

    private IEnumerator FadeTrail()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBeforeFade);

            if(trailPoints.Count > 2 && leaveTrail)
            {
                trailEnd.transform.position = trailPoints[1].transform.position;

                Destroy(trailPoints[0]);
                trailPoints.RemoveAt(0);
            }
        }
    }

    void Start()
    {
        trailPoints = new List<GameObject>();

        StartCoroutine(FadeTrail());
    }

    void FixedUpdate()
    {
        if (leaveTrail)
        {
            if (trailPoints.Count > trailCountUntilTrailEnd)
            {
                trailEnd.SetActive(true);
            }

            if (Vector2.Distance(transform.position, currentPoint.transform.position) >= distanceToNextPoint)
            {
                previousPoint = currentPoint;
                currentPoint = Instantiate(trailPointPrefab, transform.position, Quaternion.identity);

                trailPoints.Add(currentPoint);

                //if (trailPoints.Count > maxTrailLength)
                //{
                //    trailEnd.transform.position = trailPoints[1].transform.position;

                //    Destroy(trailPoints[0]);
                //    trailPoints.RemoveAt(0);
                //}

                //currentPointRenderer = currentPoint.GetComponent<LineRenderer>();
                //prevoiusPointRenderer = previousPoint.GetComponent<LineRenderer>();

                //prevoiusPointRenderer.SetPosition(0, previousPoint.transform.position);
                //prevoiusPointRenderer.SetPosition(1, transform.position);

                CapsuleCollider2D capsule = previousPoint.GetComponent<CapsuleCollider2D>();
                capsule.transform.position = previousPoint.transform.position + (transform.position - previousPoint.transform.position) / 2;
            }

            //if (currentPointRenderer != null)
            //{
            //    currentPointRenderer.SetPosition(0, currentPoint.transform.position);
            //    currentPointRenderer.SetPosition(1, transform.position);
            //}
        }
    }

    void ClearTheTrail()
    {
        oldTrailPoints = new List<GameObject>(trailPoints);
        trailPoints.Clear();

        currentPoint = null;
        //currentPointRenderer = null;

        previousPoint = null;
        //prevoiusPointRenderer = null;

        foreach (var point in oldTrailPoints)
        {
            Transform particleTransform = point.transform.GetChild(0);
            particleTransform.parent = null;

            StartCoroutine(particleTransform.GetComponent<ParticleShutOff>().ShutOff());

            Destroy(point);
        }

        foreach (var particle in leftOverParticles)
        {
            StartCoroutine(particle.GetComponent<ParticleShutOff>().ShutOff());
        }
    }

    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!AudioManager.Instance.sfxSource.isPlaying)
            {
                AudioManager.Instance.PlaySFX(trailSound);
            }

            leaveTrail = true;

            currentPoint = Instantiate(trailPointPrefab, transform.position, Quaternion.identity, trailGroup);
            //currentPointRenderer = currentPoint.GetComponent<LineRenderer>();

            trailEnd = Instantiate(trailEndPrefab, transform.position, Quaternion.identity, trailGroup);
            trailEnd.SetActive(false);

            trailPoints.Add(currentPoint);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            AudioManager.Instance.sfxSource.Stop();
            leaveTrail = false;
            Destroy(trailEnd);
        }
    }

    public void TrailClosed()
    {
        //currentPointRenderer.SetPosition(1, trailPoints[0].transform.position);

        Vector3 tempVector = currentPoint.transform.position - trailPoints[0].transform.position;

        leftOverParticles.Add(Instantiate(trailDustPrefab, trailPoints[0].transform.position + tempVector * 0.25f, Quaternion.identity));
        leftOverParticles.Add(Instantiate(trailDustPrefab, trailPoints[0].transform.position + tempVector * 0.5f, Quaternion.identity));
        leftOverParticles.Add(Instantiate(trailDustPrefab, trailPoints[0].transform.position + tempVector * 0.75f, Quaternion.identity));

        leaveTrail = false;
        Destroy(trailEnd);

        List<GameObject> monsters = trailGroup.GetComponent<CheckPoligon>().getMonstersInPoly();

        trailGroup.GetComponent<CheckPair>().EvaluatePair(monsters);
    }
}
