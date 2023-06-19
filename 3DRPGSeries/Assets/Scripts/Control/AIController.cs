using RPG.Combat;
using RPG.Core;
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
        [SerializeField] Fighter fighter;
        //cache refrence to the fighter component
        [SerializeField] Health health;
        //cache refrence to the health component
        [SerializeField] GameObject player;
        //cache refrence to the fighter component

        private void Start ()
        {
            player = GameObject.FindWithTag("Player");
            //initialises the player component upon start
        }

        private void Update()
        {
            if (health.IsDead()) { return; }
            //if the player is dead, do nothing
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
                //if the player is within the attack range, and the fighter component can attack player
            {
                print(gameObject.name + "Should chase");
                //prints the object that should be chasing the player. helpful for debugging
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
                //cancels combat if the player leaves the attack range
            }
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