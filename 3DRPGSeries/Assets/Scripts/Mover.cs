using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    //set the object that we are using as the navmesh target destination
    Ray lastRay;
    //the debug ray for tracking where the player has clicked from 2D to 3D space

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //sets the last ray to the ray created from the mouse position
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        //draw in the editor where the ray is being shot. The 100 multiplies the length of the ray to make it more visible.
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
