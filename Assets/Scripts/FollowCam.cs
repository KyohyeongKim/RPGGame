using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;

    [Range(0, 10)]
    public float dist = 10.0f;  // 목표물과 카메라 사이의 수평 거리

    [Range(0, 10)]
    public float height = 5.0f;  // 목표물과 카메라 사이의 수직 거리

    [Range(0, 10)]
    public float rotationSpeed = 5.0f;  // 얼마나 빠르게 따라갈 것인지

    private void LateUpdate()
    {
        float angle = Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        // Vector3.forward = Z값 + 1 의미
        // Vector3.up = Y값 + 1 의미
        transform.position = target.position - (rotation * Vector3.forward * dist) + (Vector3.up * height);
        transform.LookAt(target);
    }
}
