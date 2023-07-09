using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        //refrence to the objects health component
        [SerializeField] RectTransform foreground = null;
        //refrence to the foreground component
        void Update()
        {
            foreground.localScale = new Vector3(healthComponent.GetFraction(),1,1);
            //sets the new scaling factor for the foreground component based off of the fraction of health remaning.
            //(percent remaning just 0 to 1 not 0 to 100)
        }
    }
}
