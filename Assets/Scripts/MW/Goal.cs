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
    public GameObject particleFacotry;

    private Color originEmissionColor;
    private Color emissionColor;
    public bool _goalIn;
    private Coroutine brightCoroutine;

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
        /////

        brightCoroutine = StartCoroutine(Bright());       
        //////
    }
    //인보크 사용해서 점점 밝게 하기
    IEnumerator Bright() {
        int count = 0;
        float bright = 0;

        while (count < 15) {
            bright += 0.08f;
            count++;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Laser.GetColor(needColor) * bright);

            yield return new WaitForSeconds(0.1f);
        }
    }
   public void OnStageClear() {
        GameObject particle = Instantiate(particleFacotry);
        particle.transform.position = transform.position;
       // particle.transform.eulerAngles = new Vector3(180, 0, 0);
        particle.GetComponent<ParticleSystem>().Stop();
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 1);
        gameObject.SetActive(false);
    }
    private void EmissionOff()
    {
        if (brightCoroutine != null) {
            StopCoroutine(brightCoroutine);
            brightCoroutine = null;
        }
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
