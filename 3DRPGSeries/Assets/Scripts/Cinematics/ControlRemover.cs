using RPG.Combat;
using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class ControlRemover : MonoBehaviour
    {
        GameObject player;
        //cache the player object to be used in all methods

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            //gets a refrence to the player game object
        }
        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            //adds DisableControl to the list of callbacks for the .played call
            GetComponent<PlayableDirector>().stopped += EnableControl;
            //adds EnableControl to the list of callbacks for the .stopped call
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            //removes DisableControl to the list of callbacks for the .played call
            GetComponent<PlayableDirector>().stopped -= EnableControl;
            //removes EnableControl to the list of callbacks for the .stopped call
        }

        private void DisableControl(PlayableDirector pd)
            //takes a playable director, but never need to refrence it
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            //stops the players current action and from moving or attacking
            player.GetComponent<PlayerController>().enabled = false;
            //disables the player controller, preventing player input during cutscenes
        }

        private void EnableControl(PlayableDirector pd)
        //takes a playable director, but never need to refrence it
        {
            player.GetComponent<PlayerController>().enabled = true;
            //enables the player controller, allowing player input again after cutscene is completed
        }
    }
}
