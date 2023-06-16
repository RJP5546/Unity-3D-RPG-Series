using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {

        MonoBehaviour currentAction;
        //sets the current action
        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) {return; }
            //if the new action is the current action, do not continue.
            if (currentAction != null)
            {
                print("Cancelling" + currentAction);
                //if the current action isnt the previous action AND isnt null, cancel the action
            }
            currentAction = action;
            //set the current action to the new action
        }

    }
}
