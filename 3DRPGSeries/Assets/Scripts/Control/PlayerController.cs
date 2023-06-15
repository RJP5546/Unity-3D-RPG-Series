using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Mover PlayerMover;
        //set the component that we are using as the player navmesh

    private void Update()
    {
        if(Input.GetMouseButton(0))
            //GetMouseButton is true while the button is held,GetMouseButtonDown is true when pressed, must be pressed again to trigger again
        {
            MoveToCursor();
        }
        
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //sets the ray to the ray created from the mouse position
        RaycastHit hit;
            //variable for where the Raycast has hit
        bool hasHit = Physics.Raycast(ray, out hit);
            //passing in ray and hit, retrieveing out hit and storing information on where the raycast has hit into the hit var.
            //RaycastHit passes out a bool.
        Debug.DrawRay(ray.origin, ray.direction * 100);
            //draw in the editor where the ray is being shot. The 100 multiplies the length of the ray to make it more visible.
        if (hasHit)
        {
            PlayerMover.MoveTo(hit.point);
                //passes the point where the ray hits an object as a vector3
        }
    }
}
