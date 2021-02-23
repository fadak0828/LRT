using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player를 움직이고 싶다
public class PlayerMove : MonoBehaviour
{
    // Player가 움직이는 속력
    public float speed = 5;

    void Start()
    {
        // 실행시 화면창에 마우스가 안보이게 하고 싶다
        Cursor.visible = false; //커서를 화면에서 안보이게
        Cursor.lockState = CursorLockMode.Locked; //커서를 마우스 화면 중앙에 고정
    }

    void Update()
    {
        // 사용자가 WASD키를 누르면
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
        dir.y = 0;
        
        transform.position += dir * speed * Time.deltaTime;
    }
}
