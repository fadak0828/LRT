using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player를 움직이고 싶다
public class PlayerMove : MonoBehaviour
{
    
    public float speed = 5;

    void Start()
    {
        
        
    }

    void Update()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
        dir.y = 0;
        
        transform.position += dir * speed * Time.deltaTime;
    }
}
