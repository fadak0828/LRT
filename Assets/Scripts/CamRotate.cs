using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스 입력에 따라 Player 1인칭 시점에서 카메라를 회전하고 싶다
public class CamRotate : MonoBehaviour
{
    float rx, ry;
    public float rotspeed = 200;

    void Start()
    {
        
    }

   
    void Update()
    {
        // 마우스 입력에 따라
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotspeed * Time.deltaTime;
        ry += mx * rotspeed * Time.deltaTime;

        // 카메라를 회전하고 싶다
        rx = Mathf.Clamp(rx, -70, 70);
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
