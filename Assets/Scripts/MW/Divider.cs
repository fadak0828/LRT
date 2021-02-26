using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour, LaserInput
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

    public void OnLaserInput(LaserHit hit)
    {
        if (hit.color == LaserColor.YELLOW)
        {
            DividerOn(hit, LaserColor.RED, LaserColor.GREEN);
        }
        else if (hit.color == LaserColor.PURPLE)
        {
            DividerOn(hit, LaserColor.RED, LaserColor.BLUE);
        }
        else if (hit.color == LaserColor.CYAN)
        {
            DividerOn(hit, LaserColor.BLUE, LaserColor.GREEN);
        } else {
            dividerOn = false;
        }
    }

    public void OnLaserInputEnd(LaserHit hit)
    {
        output1.enabled = false;
        output2.enabled = false;
        dividerOn = false;
    }

    private void DividerOn(LaserHit hit, LaserColor color1, LaserColor color2)
    {
        output1.laserColor = color1;
        output2.laserColor = color2;

        output1.width = hit.width;
        output2.width = hit.width;

        output1.enabled = true;
        output2.enabled = true;
        dividerOn = true;
    }
}
