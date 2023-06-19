using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float ChaseDistance = 5;
        //distance from the enemy that it will chase the player
        [SerializeField] float suspicionTime = 5;
        //Time since the object last saw the player that it will remain suspicious
        [SerializeField] Fighter fighter;
        //cache refrence to the fighter component
        [SerializeField] Health health;
        //cache refrence to the health component
        [SerializeField] Mover mover;
        //cache refrence to the mover component
        [SerializeField] GameObject player;
        //cache refrence to the fighter component

        Vector3 guardPosition;
        //The vector3 location of where the AI is guarding, and should return to upon player leaving chase range.
        float timeSinceLastSawPlayer = Mathf.Infinity;
        //A float to keep track of how long it has been since the player was seen by the object.
        //Initialized at infinity because the object has yet to see the player

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
                timeSinceLastSawPlayer = 0f;
                //reset the time since the player was last seen
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            //if the player has left the attack and chase range, enter suspicion state
            {
                SuspicionBehaviour();
            }
            else
            //if cannot attack, and no longer suspicious, move back to guard state
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            //increments the time since the enemy last saw the player
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
            //movement automatically cancels combat if the player leaves the attack range
            //moves ai back to their post.
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            //stops the current action
        }

        private void AttackBehaviour()
        {
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