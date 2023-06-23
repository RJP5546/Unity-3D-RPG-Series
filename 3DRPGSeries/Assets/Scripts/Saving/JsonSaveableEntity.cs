using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class JsonSaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = "";

        // CACHED STATE
        static Dictionary<string, JsonSaveableEntity> globalLookup = new Dictionary<string, JsonSaveableEntity>();
        //a global dictionary to verify if a UUID is unique

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public JToken CaptureAsJtoken()
        {
            JObject state = new JObject();
            //creating a JObject, a special container class that allows us to store a collection of Key/Value entries.
            IDictionary<string, JToken> stateDict = state;
            //maps a Dictionary<string, JToken> right onto the JObject.  Both state and stateDict point to the same object.
            foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
            {
                JToken token = jsonSaveable.CaptureAsJToken();
                //for each component marked with component IJsonSaveable, create a new token
                string component = jsonSaveable.GetType().ToString();
                //saves the savable type as a string
                Debug.Log($"{name} Capture {component} = {token.ToString()}");
                stateDict[jsonSaveable.GetType().ToString()] = token;
                //adds to the dictionary with savable type as key, and the token as the value
            }
            return state;
        }

        public void RestoreFromJToken(JToken s)
        {
            JObject state = s.ToObject<JObject>();
            IDictionary<string, JToken> stateDict = state;
            foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
            {
                string component = jsonSaveable.GetType().ToString();
                if (stateDict.ContainsKey(component))
                {

                    Debug.Log($"{name} Restore {component} =>{stateDict[component].ToString()}");
                    jsonSaveable.RestoreFromJToken(stateDict[component]);
                }
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// runs when editing in the editor, assigns serialised objects unique identifiers after checking
        /// if it has one already or if that ID is already in use
        /// </summary>
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            //if the application is running, return
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;
            //if you are not in an active scene, return (ignores prefab editors as they do not have a scene path)

            SerializedObject serializedObject = new SerializedObject(this);
            //finds the serialization of this monobehavior
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            //gets the property for the uniqueIdentifier string

            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                //generates a UUID
                serializedObject.ApplyModifiedProperties();
                //Tells unity the serialised object was modified
            }

            globalLookup[property.stringValue] = this;
            //adds the unique UUID to the global dictionary
        }
#endif

        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;
            //if the key is already in the dictionary
            if (globalLookup[candidate] == this) return true;
            //if this key is already assigned to this object
            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            //if the game object was destroyed and is no longer referenced

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            //if the object no longer matches the ID, (the dictionary is out of date)

            return false;
        }


    }
}
