using RPG.Combat;
using RPG.Attributes;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
    //Namespaces prevent overlapping of class names, need to add in using statements to refrence namespaced classes
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Mover PlayerMover;
        //set the component that we are using as the player navmesh
        [SerializeField] Health health;
        //cache refrence to the health component

        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) { return; }
            //if the player is dead, do nothing
            if (InteractWithCombat()) { return; }
            //if InteractWithCombat is true, ignore movement.
            if (InteractWithMovement()) { return; }
            //if InteractWithMovement is true, allow movement, if not, it will continue and say there is no action the player can take
            print("Nothing to do.");

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            //puts everything the ray hits into an array
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                //every object has a transform component, and a component can see any other sibling component on an object
                if (target == null) {continue; }
                //if there is no combat target, make it not clickable
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) { continue; }
                //if we cant attack,conntine in the foreach loop (going to the next thing in array)
                if(Input.GetMouseButton(0)) 
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
                //returns true even for hovering over a target
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            //variable for where the Raycast has hit
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            //passing in ray and hit, retrieveing out hit and storing information on where the raycast has hit into the hit var.
            //RaycastHit passes out a bool.
            Debug.DrawRay(GetMouseRay().origin, GetMouseRay().direction * 100);
            //draw in the editor where the ray is being shot. The 100 multiplies the length of the ray to make it more visible.
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                //GetMouseButton is true while the button is held,GetMouseButtonDown is true when pressed, must be pressed again to trigger again
                {
                    PlayerMover.StartMoveAction(hit.point, 1f);
                    //passes the point where the ray hits an object as a vector3, as well as player full speed
                }
                return true;
                //if the ray finds an object the player can interact with, return true
            }
            return false;
            //if the ray does not find an object the player can interact with, return false
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            //returns the ray created from the mouse
        }
    }
}
