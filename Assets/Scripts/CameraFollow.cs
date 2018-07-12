using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float Smoothing;

    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        if(objectToFollow != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectToFollow.transform.position + offset, Smoothing);
            transform.position = newPosition;
        }
    }
}
