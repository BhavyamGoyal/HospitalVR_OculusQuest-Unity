using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using HCFramework;

public class BloodCellScript : MonoBehaviour
{

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed;
    GameObject cell;
    Vector3 rotation, position;

    float dsTravelled;
    private void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("BloodPath").GetComponent<PathCreator>();
        position = new Vector3(((float)Random.Range(-100, 100) / (float)100), ((float)Random.Range(-100, 100) / (float)100), ((float)Random.Range(-100, 100) / (float)100));
        cell = this.transform.FindChildGameObject("cell", false);
        cell.transform.position = position;
        rotation = new Vector3(Random.Range(0, 20), Random.Range(0, 20), Random.Range(0, 20)).normalized;
        speed = ((float)Random.Range(70, 100) / (float)100);
    }
    private void Update()
    {
        cell.transform.Rotate(rotation);
        dsTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dsTravelled, end);
    }
}