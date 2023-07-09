using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        private void Update()
        {
            transform.forward = Camera.main.transform.forward;
            //sets the transform of the object to the cameras forward vector, facing the object the same way as the camera
        }
    }

}