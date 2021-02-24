using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour, LaserInput
{
    public float width = 0.1f;
    public LaserShooter output1;
    public LaserShooter output2;

    private bool dividerOn = false;

    public void OnLaserInput(LaserHit hit) {
        if (dividerOn == false) {
            output1.laserColor = hit.color;
            output2.laserColor = hit.color;

            output1.width = hit.width;
            output2.width = hit.width;

            output1.enabled = true;
            output2.enabled = true;
            dividerOn = true;
        }
    }

    public void OnLaserInputEnd(LaserHit hit) {
        output1.enabled = false;
        output2.enabled = false;
        dividerOn = false;
    }
}
