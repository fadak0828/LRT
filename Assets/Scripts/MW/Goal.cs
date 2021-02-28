using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Goal : MonoBehaviour, LaserInput
{
    public LaserColor needColor;
    public GameObject needColorObj;
    public GameObject redMarker;
    public GameObject blueMarker;
    public GameObject greenMarker;
    public bool goalIn;

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

    public void OnLaserInput(LaserHit hit) {
        goalIn = hit.color == needColor;
    }
    public void OnLaserInputEnd(LaserHit hit) {
        goalIn = false;
    }
}
