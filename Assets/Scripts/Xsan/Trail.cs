using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject trailPointPrefab;
    public GameObject trailEndPrefab;
    public Transform trailGroup;

    public float distanceToNextPoint;
    public int maxTrailLength;
    public int trailCountUntilTrailEnd;

    public List<GameObject> trailPoints;
    public List<GameObject> oldTrailPoints;

    private GameObject trailEnd;

    private GameObject currentPoint;
    private GameObject previousPoint;

    private LineRenderer currentPointRenderer;
    private LineRenderer prevoiusPointRenderer;

    public bool leaveTrail;

    void Start()
    {
        trailPoints = new List<GameObject>();
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

                if (trailPoints.Count > maxTrailLength)
                {
                    trailEnd.transform.position = trailPoints[1].transform.position;

                    Destroy(trailPoints[0]);
                    trailPoints.RemoveAt(0);
                }

                currentPointRenderer = currentPoint.GetComponent<LineRenderer>();
                prevoiusPointRenderer = previousPoint.GetComponent<LineRenderer>();

                prevoiusPointRenderer.SetPosition(0, previousPoint.transform.position);
                prevoiusPointRenderer.SetPosition(1, transform.position);

                CapsuleCollider2D capsule = prevoiusPointRenderer.GetComponent<CapsuleCollider2D>();
                capsule.transform.position = previousPoint.transform.position + (transform.position - previousPoint.transform.position) / 2;
            }

            if (currentPointRenderer != null)
            {
                currentPointRenderer.SetPosition(0, currentPoint.transform.position);
                currentPointRenderer.SetPosition(1, transform.position);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            leaveTrail = true;

            oldTrailPoints = new List<GameObject>(trailPoints);
            trailPoints.Clear();

            currentPoint = null;
            currentPointRenderer = null;

            previousPoint = null;
            prevoiusPointRenderer = null;

            foreach (var point in oldTrailPoints)
            {
                Destroy(point);
            }

            //

            currentPoint = Instantiate(trailPointPrefab, transform.position, Quaternion.identity, trailGroup);
            currentPointRenderer = currentPoint.GetComponent<LineRenderer>();

            trailEnd = Instantiate(trailEndPrefab, transform.position, Quaternion.identity, trailGroup);
            trailEnd.SetActive(false);

            trailPoints.Add(currentPoint);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            leaveTrail = false;
            Destroy(trailEnd);
        }
    }

    public void TrailClosed()
    {
        currentPointRenderer.SetPosition(1, trailPoints[0].transform.position);

        leaveTrail = false;
        Destroy(trailEnd);

        List<GameObject> monsters = trailGroup.GetComponent<CheckPoligon>().getMonstersInPoly();

        trailGroup.GetComponent<CheckPair>().EvaluatePair(monsters);
    }
}
