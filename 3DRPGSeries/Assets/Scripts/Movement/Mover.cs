using Newtonsoft.Json.Linq;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
//core namespaces apply to Movement scripts
{
    public class Mover : MonoBehaviour, IAction, IJsonSaveable
    {
        [SerializeField] NavMeshAgent navMeshAgent;
        //set the component that we are using as the object navmesh
        [SerializeField] Animator animator;
        //set the component that we are using as the object animator
        [SerializeField] Health health;
        //set the component that we are using as the object health
        [SerializeField] float maxSpeed = 5.66f;
        //set the max speed float

        private readonly int ForwardSpeedHash = Animator.StringToHash("ForwardSpeed");
        //store ForwardSpeed animator value as a hash for faster refrence



        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.isStopped = false;
            //enables player movement
            navMeshAgent.destination = destination;
            //sets the players navmesh agent destination to the point where the ray hits an object
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            //sets the nav mesh agent speed to max * any fraction multiplier. Clamp01() restricts the input var to between 0 and 1.
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

        public JToken CaptureAsJToken()
        {
            return transform.position.ToToken();
        }

        public void RestoreFromJToken(JToken state)
        {
            navMeshAgent.enabled = false;
            transform.position = state.ToVector3();
            navMeshAgent.enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

    }
}
