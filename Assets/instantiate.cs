using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiate : MonoBehaviour
{
    RaycastHit hit;
    public Transform prefab;
    int count = 5;
    // Start is called before the first frame update
    void Start()
    {
            function2();
        
    }
    // Update is called once per frame
    void Update()
    {

    }


    void function1()
    {
        Vector3 down = new Vector3(0, -1, 0);
        if (Physics.Raycast(transform.position, down, out hit))
        {
            float distanceToGround = hit.distance;
            Vector3 currentPos = transform.position;
            float newY = currentPos.y - distanceToGround;
            transform.position = new Vector3(currentPos.x, newY, currentPos.z);
           // Quaternion.FromToRotation(Vector3.up, normal);
        }
    }

    void function2()
    {
        float randomX;
        float randomZ;

        Renderer r = gameObject.GetComponentsInChildren<Renderer>()[5];  // assumes the terrain is in a mesh renderer on the same GameObject

        Debug.Log(r + "" + r.bounds.min + "hiihihi" + r.bounds.max);
        while (count > 0)
        {
            randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
            randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);
            if (Physics.Raycast(new Vector3(randomX, r.bounds.max.y + 2f, randomZ), -Vector3.up, out hit))
            {
                Debug.Log("hhit");
                // the raycast hit, the point on the terrain is hit.point, spawn a tree there
                Instantiate(prefab, hit.point, Quaternion.identity);
                count--;
            }
            else
            {
                // the raycast didn't hit, maybe there's a hole in the terrain?
                Debug.Log("ray not hhit");
            }

        }
    }

}