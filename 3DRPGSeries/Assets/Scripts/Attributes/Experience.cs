using Newtonsoft.Json.Linq;
using RPG.Saving;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float experiencePoints = 0;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            //adds the passed experience to the experience total
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
