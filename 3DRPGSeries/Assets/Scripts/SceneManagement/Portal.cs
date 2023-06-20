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
            Destroy(gameObject);
            //destroys portal when its no longer needed, preventing unwanted overlap
        }
    }
}
