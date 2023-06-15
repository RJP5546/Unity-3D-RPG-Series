using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
//core namespaces apply to Movement scripts
{
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
            UpdateAnimator();
        }



        public void MoveTo(Vector3 destination)
        {
            PlayerNavMesh.destination = destination;
            //sets the players navmesh agent destination to the point where the ray hits an object
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
}
