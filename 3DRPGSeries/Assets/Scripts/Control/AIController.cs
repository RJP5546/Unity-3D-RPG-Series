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

        private void Update()
        {
            if(DistanceToPLayer() < ChaseDistance)
                //if the player is within the chase distance
            {
                print(gameObject.name + "Should chase");
                //prints the object that should be chasing the player. helpful for debugging
            }
        }

        private float DistanceToPLayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            //finds the game onject with player tag, returns game object
            return Vector3.Distance(player.transform.position, transform.position);
            //returns the distance form the player game object to the self position
        }
    }

}