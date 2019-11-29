using HCFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5);
    }
    private void OnTriggerEnter(Collider other)
    {
       
            Utils.EventAsync(new BulletHitEvent(this.gameObject,other.gameObject));
        
       // Debug.Log("bullet hit" + other.gameObject.transform.parent.name);
    }
}
