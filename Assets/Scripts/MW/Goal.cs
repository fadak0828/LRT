using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour, LaserInput
{
    public LaserColor needColor;
    public bool goalIn;

    public void OnLaserInput(LaserHit hit) {
        goalIn = hit.color == needColor;
    }
    public void OnLaserInputEnd(LaserHit hit) {
        goalIn = false;
    }
}
