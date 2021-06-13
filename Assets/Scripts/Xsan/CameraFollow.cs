using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y, -500);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(objectToFollow.position.x, objectToFollow.position.y, -500), 0.05f);
    }
}