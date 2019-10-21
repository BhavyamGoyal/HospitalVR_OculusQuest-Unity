using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraViewMouseControlled : MonoBehaviour {
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public void Start () {
       
    }
    // Update is called once per frame
    void Update () {
        Screen.lockCursor = true;
        Cursor.visible = true;
         yaw += speedH * Input.GetAxis ("Mouse X");
        pitch -= speedV * Input.GetAxis ("Mouse Y");

        transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);

    }
}