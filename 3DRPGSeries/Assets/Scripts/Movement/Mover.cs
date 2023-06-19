using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
//core namespaces apply to Movement scripts
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] NavMeshAgent navMeshAgent;
        //set the component that we are using as the object navmesh
        [SerializeField] Animator animator;
        //set the component that we are using as the object animator
        [SerializeField] Health health;
        //set the component that we are using as the object health
        private readonly int ForwardSpeedHash = Animator.StringToHash("ForwardSpeed");
        //store ForwardSpeed animator value as a hash for faster refrence



        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }



        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.isStopped = false;
            //enables player movement
            navMeshAgent.destination = destination;
            //sets the players navmesh agent destination to the point where the ray hits an object
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
            //stops the player movement
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            //Gets the player nev mesh agent's global velocity
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //takes grom global velocity to local velocity. This is because the aniomator only cares about the local
            //velocity, regardless of position in the world
            float speed = localVelocity.z;
            //Sets the float speed value to the local forward(z) velocity
            animator.SetFloat(ForwardSpeedHash, speed);
            //Sets the animators speed value to the value of float speed

        }
    }
}
