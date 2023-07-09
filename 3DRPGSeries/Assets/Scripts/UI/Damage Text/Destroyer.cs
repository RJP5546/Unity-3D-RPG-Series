using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] GameObject targetToDestroy = null;

    public void DestroyTarget()
    {
        Destroy(targetToDestroy);
        //destroys the target on event call, used for the damage text animation
    }
}
