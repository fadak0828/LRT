using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Goal : LaserInput
{
    public LaserColor needColor;
    public GameObject needColorObj;
    public GameObject redMarker;
    public GameObject blueMarker;
    public GameObject greenMarker;

    private bool _goalIn;
    public bool goalIn {
        get { return _goalIn; }
        set {
            _goalIn = value;
            if (goalIn) {
                EmissionOn();
            } else {
                EmissionOff();
            }
        }
    }

    private void Awake() {
        redMarker.SetActive(((int)needColor & (int)LaserColor.RED) != 0);    
        blueMarker.SetActive(((int)needColor & (int)LaserColor.BLUE) != 0);    
        greenMarker.SetActive(((int)needColor & (int)LaserColor.GREEN) != 0);

        Shader shader = Shader.Find("Standard");
        Shader.EnableKeyword("_EMISSION");
        
        Material mat = new Material(shader);
        mat.SetColor("_EmissionColor", Laser.GetColor(needColor));

        needColorObj.GetComponent<Renderer>().sharedMaterial = mat;
    }

    private void EmissionOn() {

    }

    private void EmissionOff() {

    }

    override public void OnLaserInput(LaserHit hit) {
        goalIn = hit.color == needColor;
    }
    override public void OnLaserInputEnd(LaserHit hit) {
        goalIn = false;
    }
}
