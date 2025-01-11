using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform;
    [Range(1, 10)] public float followSpeed = 2;
    [Range(1, 10)] public float lookSpeed = 5;

    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset relative to the car's rotation
        offset = carTransform.InverseTransformPoint(transform.position);
    }

    void FixedUpdate()
    {
        // Calculate the desired position relative to the car's current rotation
        Vector3 targetPosition = carTransform.TransformPoint(offset);

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate the camera to look at the car
        Vector3 lookDirection = carTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }
}
