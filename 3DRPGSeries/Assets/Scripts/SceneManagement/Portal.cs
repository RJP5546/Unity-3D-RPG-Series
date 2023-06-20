using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        //set the int for the scene to load from the build index
        

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
                //if the player collides with the trigger
            {
                SceneManager.LoadScene(sceneToLoad);
                //loads next scene based off of load index value inputted
            }
        }
    }
}
