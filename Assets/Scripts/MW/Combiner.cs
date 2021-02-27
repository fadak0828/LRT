using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour, LaserInput
{
    public int colorMask = 0;
    public List<LaserColor> needColors;
    public LaserShooter output;
    private List<LaserHit> hitList;
    void Start()
    {
        hitList = new List<LaserHit>();
        output.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (colorMask != 0) {
            output.gameObject.SetActive(true);
            output.laserColor = (LaserColor)colorMask;
        } else {
            output.gameObject.SetActive(false);
        }
    }

    public void OnLaserInput(LaserHit hit) {
        colorMask |= (int)hit.color;
    }

    public void OnLaserInputEnd(LaserHit hit) {
        colorMask ^= (int)hit.color;
    }
}
