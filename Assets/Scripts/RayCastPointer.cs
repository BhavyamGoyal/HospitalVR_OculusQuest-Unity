using HCFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPointer : MonoBehaviour
{
    LineRenderer laser;
    RaycastHit hit;
    GameObject dot=null,dotPrefab;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        dotPrefab = Resources.Load<GameObject>("target");
        mask = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 200))
        {
            
            laser.SetPosition(0, transform.position);
           // Debug.Log("Did Hit0");
            laser.SetPosition(1, hit.point);
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit1");

            if (dot==null){
                dot = Instantiate(dotPrefab, null);
            //Debug.Log("Did Hit2");
                Utils.EventAsync(new GetDotEvent(dot));
            }
            dot.transform.LookAt(transform);
            // dot.SetActive(true);

            dot.transform.position = hit.point;//new Vector3(hit.point.x, hit.point.y, hit.point.z-(hit.distance*.05f));
            //Debug.Log("Did Hit3");
        }
        
        
    }
}
