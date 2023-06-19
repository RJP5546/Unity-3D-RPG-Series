using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;
        //sets a constant size of the waypoint gizmo for viewing in the editor
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++) 
                //for each child component
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmoRadius);
                //draw a sphere at the child component
            }
            
        }
    }
}
