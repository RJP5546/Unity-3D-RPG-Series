using Newtonsoft.Json.Linq;
using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float experiencePoints = 0;

        public event Action onExpierenceGained;
        //created a event for on expierence gained

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            //adds the passed experience to the experience total
            onExpierenceGained();
        }
        public float GetExperience()
        {
            return experiencePoints;
            //a public method to refrence amount of experience points
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(experiencePoints);
            //save the amount of experience points
        }

        public void RestoreFromJToken(JToken state)
        {
            experiencePoints = state.ToObject<float>();
            //load the amount of experience points
        }
    }
}
