using RPG.Combat;
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
        [SerializeField] GameObject player;
        //cache refrence to the fighter component

        private void Start ()
        {
            fighter = GetComponent<Fighter>();
            //initialises the fighter compoinent upon start.
            player = GameObject.FindWithTag("Player");
            //initialises the player component upon start
        }

        private void Update()
        {
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
    }

}