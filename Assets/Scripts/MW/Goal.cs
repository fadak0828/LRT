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

    private Color originEmissionColor;
    private Color emissionColor;
    public bool _goalIn;
    public bool goalIn
    {
        get { return _goalIn; }
        set
        {
            if (_goalIn == false && value == true)
            {
                EmissionOn();
                AudioSource audio = GetComponent<AudioSource>();
                audio.Stop();
                audio.Play();
                print("Goal In");
                _goalIn = value;
                return;
            }

            if (_goalIn == true && value == false)
            {
                _goalIn = false;
                EmissionOff();
                print("Goal out");
                return;
            }
        }
    }

    private void Awake()
    {
        redMarker.SetActive(((int)needColor & (int)LaserColor.RED) != 0);
        blueMarker.SetActive(((int)needColor & (int)LaserColor.BLUE) != 0);
        greenMarker.SetActive(((int)needColor & (int)LaserColor.GREEN) != 0);

        Shader shader = Shader.Find("Standard");
        Shader.EnableKeyword("_EMISSION");

        Material mat = new Material(shader);
        mat.SetColor("_EmissionColor", Laser.GetColor(needColor));

        needColorObj.GetComponent<Renderer>().sharedMaterial = mat;

        originEmissionColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        goalIn = false;
    }

    private void EmissionOn()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Laser.GetColor(needColor) * 0.8f);
    }

    private void EmissionOff()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
    }

    override public void OnLaserInput(LaserHit hit)
    {
        bool nextGoalInState = hit.color == needColor;
        if (nextGoalInState != goalIn)
        {
            goalIn = nextGoalInState;
        }
    }
    override public void OnLaserInputEnd(LaserHit hit)
    {
        goalIn = false;
    }
}
