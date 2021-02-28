﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : LaserInput
{
    public float width = 0.1f;
    public LaserShooter output1;
    public LaserShooter output2;

    public bool dividerOn = false;

    private void Update()
    {
        if (dividerOn)
        {
            output1.enabled = true;
            output2.enabled = true;
        }
        else
        {
            output1.enabled = false;
            output2.enabled = false;
        }
    }

    override public void OnLaserInput(LaserHit hit)
    {
        switch (hit.color)
        {
            case LaserColor.YELLOW:
                DividerOn(hit, LaserColor.RED, LaserColor.GREEN);
                break;
            case LaserColor.PURPLE:
                DividerOn(hit, LaserColor.RED, LaserColor.BLUE);
                break;
            case LaserColor.CYAN:
                DividerOn(hit, LaserColor.BLUE, LaserColor.GREEN);
                break;
            default:
                DivderOff();
                break;
        }
    }

    override public void OnLaserInputEnd(LaserHit hit)
    {
        DivderOff();
    }

    private void DivderOff()
    {
        dividerOn = false;
    }

    private void DividerOn(LaserHit hit, LaserColor color1, LaserColor color2)
    {
        output1.laserColor = color1;
        output2.laserColor = color2;

        output1.width = hit.width;
        output2.width = hit.width;

        dividerOn = true;
    }
}
