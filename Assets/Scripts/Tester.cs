using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Fader.instance.FadeOut(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fader.instance.Fadein(1);

        }
    }
}
