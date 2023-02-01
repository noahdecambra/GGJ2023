using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = newPosition;
    }
}
