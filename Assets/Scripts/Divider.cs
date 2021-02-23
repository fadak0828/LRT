using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour, LaserInput
{
    public GameObject inputSurface;
    public Laser output1;
    public Laser output2;
    public LaserColor laserColor = LaserColor.NONE;

    public void OnInputStart(Laser laser) {
        laserColor = laser.laserColor;
        output1.gameObject.SetActive(true);
        output1.laserColor = laserColor;
        output1.width = laser.width;

        output2.gameObject.SetActive(true);
        output2.laserColor = laserColor;
        output2.width = laser.width;
    }

    public void OnInputEnd(Laser laser) {
        laserColor = LaserColor.NONE;
        output1.gameObject.SetActive(false);
        output2.gameObject.SetActive(false);
    }
}
