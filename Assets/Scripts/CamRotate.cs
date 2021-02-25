using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player의 시점에서 Cam을 회전시키고 싶다
public class CamRotate : MonoBehaviour
{
    // rx, ry 축값
    float rx, ry;
    // 회전속도
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
