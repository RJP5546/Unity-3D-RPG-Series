using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;
        //sets if the animation has already been played
        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.gameObject.tag == "Player") 
            {
                alreadyTriggered = true;
                //sets the animation to already played, so it wont be played twice
                GetComponent<PlayableDirector>().Play();
                //Plays the PlayableDirector component attached to this game object
            }

        }
    }
}
