using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamRotate : MonoBehaviour
{
    float rx, ry;
    public float rotspeed = 200;

    void Start()
    {
        
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += my * rotspeed * Time.deltaTime;
        ry += mx * rotspeed * Time.deltaTime;

        rx = Mathf.Clamp(rx, -70, 70);
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
