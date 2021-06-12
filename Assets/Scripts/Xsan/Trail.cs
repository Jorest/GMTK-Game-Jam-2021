using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject trailPointPrefab;
    public Transform trailGroup;
    public float distanceToNextPoint;

    public List<GameObject> trailPoints;
    public int maxTrailLength;

    private GameObject currentPoint;
    private GameObject previousPoint;

    private LineRenderer currentPointRenderer;
    private LineRenderer prevoiusPointRenderer;

    void Start()
    {
        currentPoint = Instantiate(trailPointPrefab, transform.position, Quaternion.identity, trailGroup);
        currentPointRenderer = currentPoint.GetComponent<LineRenderer>();

        trailPoints = new List<GameObject>();
        trailPoints.Add(currentPoint);
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, currentPoint.transform.position) >= distanceToNextPoint)
        {
            previousPoint = currentPoint;
            currentPoint = Instantiate(trailPointPrefab, transform.position, Quaternion.identity);

            trailPoints.Add(currentPoint);

            if(trailPoints.Count > maxTrailLength)
            {

                Destroy(trailPoints[0]);
                trailPoints.RemoveAt(0);
            }

            currentPointRenderer = currentPoint.GetComponent<LineRenderer>();
            prevoiusPointRenderer = previousPoint.GetComponent<LineRenderer>();

            prevoiusPointRenderer.SetPosition(0, previousPoint.transform.position);
            prevoiusPointRenderer.SetPosition(1, transform.position);
        }

        if(currentPointRenderer != null)
        {
            currentPointRenderer.SetPosition(0, currentPoint.transform.position);
            currentPointRenderer.SetPosition(1, transform.position);
        }
    }
}
