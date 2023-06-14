using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        transform.position = target.position;
            //sets the camera position to the position of the target
    }
}
