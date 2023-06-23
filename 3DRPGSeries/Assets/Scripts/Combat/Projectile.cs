using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(target == null) {  return; }
        //if no target, return
        transform.LookAt(GetAimLocation());
        //look at the projectiles target
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //moves the projectile on the z axis at a set speed over time

    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) 
        {
            return target.position;
            //if no capsule collider return the targets default position
        }
        return target.position + Vector3.up* targetCapsule.height/2;
            //Vector3.up is short for writing Vector3(0,1,0), basically adding 1 on the y axis
            //we then add 1 * capsule height / 2 to find the midle of the capsule
    }
}
