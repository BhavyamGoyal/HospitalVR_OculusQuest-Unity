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
        if (other.gameObject.transform.parent.name.Equals("Cancer"))
        {
            Utils.EventAsync(new BulletHitEvent());
        }
        Debug.Log("bullet hit" + other.gameObject.transform.parent.name);
    }
}
