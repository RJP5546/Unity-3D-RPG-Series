using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    Health target = null;
    //the target for the projectile
    float damage = 0;
    //projectile damage
    void Update()
    {
        if(target == null) {  return; }
        //if no target, return
        transform.LookAt(GetAimLocation());
        //look at the projectiles target
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //moves the projectile on the z axis at a set speed over time

    }
    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        //sets the projectiles target to the passed in target
        this.damage = damage;
        //sets the projectile damage to the passed in damage
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) 
        {
            return target.transform.position;
            //if no capsule collider return the targets default position
        }
        return target.transform.position + Vector3.up* targetCapsule.height/2;
            //Vector3.up is short for writing Vector3(0,1,0), basically adding 1 on the y axis
            //we then add 1 * capsule height / 2 to find the midle of the capsule
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != target) {return;}
        //if the object hit is not our target, ignore
        target.TakeDamage(damage);
        //if it is the right target, deal damage
        Destroy(gameObject);
        //destroy the shot projectile
    }
}
