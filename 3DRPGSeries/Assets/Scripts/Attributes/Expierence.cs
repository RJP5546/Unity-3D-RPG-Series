using UnityEngine;

namespace RPG.Attributes
{
    public class Expierence : MonoBehaviour
    {
        [SerializeField] float expierencePoints = 0;

        public void GainExpierence(float expierence)
        {
            expierencePoints += expierence;
            //adds the passed expierence to the expierence total
            print(expierencePoints);
        }
    }
}
