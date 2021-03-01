using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : LaserInput
{
    public int colorMask = 0;
    public LaserShooter output;
    void Start()
    {
        output.gameObject.SetActive(false);
    }

    void Update()
    {
        if (colorMask != 0) {
            output.gameObject.SetActive(true);
            output.laserColor = (LaserColor)colorMask;
        } else {
            output.gameObject.SetActive(false);
        }
    }

    override public void OnLaserInput(LaserHit hit) {
        colorMask |= (int)hit.color;
    }

    override public void OnLaserInputEnd(LaserHit hit) {
        colorMask ^= (int)hit.color;
    }
}
