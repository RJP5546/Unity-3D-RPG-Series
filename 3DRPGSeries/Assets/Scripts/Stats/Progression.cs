using RPG.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
//adds a new interface option into the editor when you right click, can now create new progression scriptable object
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;
    //select the character class from the editor to mark what progression tree the object will follow

    [System.Serializable]
    //marks the ProgressionCharacterClass as serializable, and makes the classes appear in the editor
    class ProgressionCharacterClass
    {
        [SerializeField] CharacterClass characterClass;
        //select the character class
        [SerializeField] float[] health;
        //health array for health values as object levels up
    }
}