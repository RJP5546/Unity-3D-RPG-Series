using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] NavMeshAgent PlayerNavMesh;
        //set the component that we are using as the player navmesh
    [SerializeField] Animator PlayerAnimator;
        //set the component that we are using as the player animator
    private readonly int ForwardSpeedHash = Animator.StringToHash("ForwardSpeed");
        //store ForwardSpeed animator value as a hash for faster refrence

    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //sets the ray to the ray created from the mouse position
        RaycastHit hit;
            //variable for where the Raycast has hit
        bool hasHit = Physics.Raycast(ray, out hit);
            //passing in ray and hit, retrieveing out hit and storing information on where the raycast has hit into the hit var.
            //RaycastHit passes out a bool.
        Debug.DrawRay(ray.origin, ray.direction * 100);
            //draw in the editor where the ray is being shot. The 100 multiplies the length of the ray to make it more visible.
        if (hasHit)
        {
            PlayerNavMesh.destination = hit.point;
                //sets the players navmesh agent destination to the point where the ray hits an object
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = PlayerNavMesh.velocity;
            //Gets the player nev mesh agent's global velocity
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //takes grom global velocity to local velocity. This is because the aniomator only cares about the local
            //velocity, regardless of position in the world
        float speed = localVelocity.z;
            //Sets the float speed value to the local forward(z) velocity
        PlayerAnimator.SetFloat(ForwardSpeedHash, speed);
            //Sets the animators speed value to the value of float speed

    }
}
