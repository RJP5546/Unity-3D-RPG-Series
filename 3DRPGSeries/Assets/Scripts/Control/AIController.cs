using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using System;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] Fighter fighter;
        //cache refrence to the fighter component
        [SerializeField] Health health;
        //cache refrence to the health component
        [SerializeField] Mover mover;
        //cache refrence to the mover component
        [SerializeField] GameObject player;
        //cache refrence to the fighter component
        [SerializeField] PatrolPath patrolPath;
        //cache refrence to the objects patrol path, can be null
        [SerializeField] float ChaseDistance = 5f;
        //distance from the enemy that it will chase the player
        [SerializeField] float suspicionTime = 5f;
        //Time since the object last saw the player that it will remain suspicious
        [SerializeField] float waypointTolerance = 1f;
        //The level of variance in detecting if the object is at its target waypoint 
        [SerializeField] float waypointDwellTime = 1f;
        //Time the object will dwell at at patrol point
        [Range(0f, 1f)]
        //the range of patrolSpeedFraction can only be between 0, and 1
        [SerializeField] float patrolSpeedFraction = 0.2f;
        //The multiplier applied to the max speed of the navmesh agent during patroling


        Vector3 guardPosition;
        //The vector3 location of where the AI is guarding, and should return to upon player leaving chase range.
        float timeSinceLastSawPlayer = Mathf.Infinity;
        //A float to keep track of how long it has been since the player was seen by the object.
        //Initialized at infinity because the object has yet to see the player
        int currentWaypointIndex = 0;
        //int that tracks what index of the patrol path the object is currently on
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        //A float to keep track of how long it has been since the object has arrived at its current waypoint.

        private void Start ()
        {
            player = GameObject.FindWithTag("Player");
            //initialises the player component upon start
            guardPosition = transform.position;
            //sets the guard position to the objects initial position upon game start
        }

        private void Update()
        {
            if (health.IsDead()) { return; }
            //if the player is dead, do nothing
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            //if the player is within the attack range, and the fighter component can attack player, enter attack state
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            //if the player has left the attack and chase range, enter suspicion state
            {
                SuspicionBehaviour();
            }
            else
            //if cannot attack, and no longer suspicious, move back to guard state
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            //increments the time since the enemy last saw the player
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            //increments the time since the object arrived at the current waypoint
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            //starts the next position as the guard position so the guard starts at the guard point
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > waypointDwellTime) 
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
                //movement automatically cancels combat if the player leaves the attack range
                //moves ai back to their post, passes patrolSpeedFraction to their NavMesh Agent
            }
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            //calls GetNextIndex() method in PatrolPath, getting the next index in the list of waypoints.
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
            //calls the GetWaypoint() method in PatrolPath, and returns the index of the current waypoint
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            //sets the distance from the object to the waypoint as a float value
            return distanceToWaypoint < waypointTolerance;
            //returns if the object is close enough to be within the level of alloted tolerance, returns true or false.
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            //stops the current action
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0f;
            //reset the time since the player was last seen
            fighter.Attack(player);
            //Calls the Attack() method from the fighter script
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            //gets the distance from the player game object to the self position
            return distanceToPlayer < ChaseDistance;
            //returns if distanceToPlayer is less than ChaseDistance
        }

        private void OnDrawGizmosSelected()
            //Called by unity, for use in the editor, only shows when the object is selected
        {
            Gizmos.color = Color.blue;
            //sets the sphere color to blue
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
            //draws a wire sphere that visualises the chase radius of the enemy
        }
    }

}