using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum OutputColor {
    RED = 1 << (int)LaserColor.RED,
    BLUE = 1 << (int)LaserColor.BLUE,
    GREEN = 1 << (int)LaserColor.GREEN, 
    YELLOW = 1 << (int)LaserColor.GREEN | 1 << (int)LaserColor.RED,
    WHITE = 1 << (int)LaserColor.GREEN | 1 << (int)LaserColor.RED | 1<< (int)LaserColor.BLUE,
    CYAN = 1 << (int)LaserColor.GREEN | 1 << (int)LaserColor.BLUE,
    PURPLE = 1 << (int)LaserColor.BLUE | 1 << (int)LaserColor.RED,
}

public class Combiner : MonoBehaviour, LaserInput
{
    public int inputColors = 0;
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
        if (inputColors != 0) {
            output.gameObject.SetActive(true);
            output.laserColor = GetOutputColor();
        } else {
            output.gameObject.SetActive(false);
        }
    }

    private LaserColor GetOutputColor() {
        switch(inputColors) {
            case (int)OutputColor.CYAN:
                return LaserColor.CYAN;
            case (int)OutputColor.PURPLE:
                return LaserColor.PURPLE;
            case (int)OutputColor.YELLOW:
                return LaserColor.YELLOW;
            case (int)OutputColor.WHITE:
                return LaserColor.WHITE;
            case (int)OutputColor.RED:
                return LaserColor.RED;
            case (int)OutputColor.BLUE:
                return LaserColor.BLUE;
            case (int)OutputColor.GREEN:
                return LaserColor.GREEN;
            default:
                return LaserColor.NONE;
        }
    }

    public void OnLaserInput(LaserHit hit) {
        inputColors |= 1 << (int)hit.color;
    }

    public void OnLaserInputEnd(LaserHit hit) {
        inputColors &= 0 << (int)hit.color;
    }
}
