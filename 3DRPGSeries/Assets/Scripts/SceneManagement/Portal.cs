using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B,C,D,E,F,G,H
        }

        [SerializeField] int sceneToLoad;
        //set the int for the scene to load from the build index
        [SerializeField] Transform spawnPoint;
        //set the transform component of the spawn point gameobject
        [SerializeField] DestinationIdentifier destination;
        //set the destination target


        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
                //if the player collides with the trigger
            {
                StartCoroutine(Transition());
                //start the Transition() coroutine
            }
        }
        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            //dont destroy the portal game object on load
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            //loads next scene based off of load index value inputted, returns async operation when the scene has finished loading,
            //calling the coroutine again.
            print("Scene Loaded");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);


            Destroy(gameObject);
            //destroys portal when its no longer needed, preventing unwanted overlap
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            //create a refrence to the player game object
            player.transform.position = otherPortal.spawnPoint.position;
            //set the player transform to the transform of the spawnpoint
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            //set the player rotation to the rotation of the spawnpoint
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
                //Finds all the objects of type portal and turns them into a list.
            {
                if (portal == this) { continue; }
                //if the portal is the current portal, continue to the next portal in the list
                if (portal.destination != destination) { continue; }
                //if the destination portal is the current portal, continue to the next portal in the list
                return portal;
                //return the portal
            }
            return null;
            //if no portals, retun null
        }
    }
}
