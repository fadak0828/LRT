using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObject : LaserInput {
    public GameObject turnObject;
    public GameObject particleSystem;
    Material objectLight;
    Color bright = new Color(0, 0, 0);

    float currntTime;
    int count =0;

    private void Start() {
        particleSystem.SetActive(false);
        objectLight = GetComponent<MeshRenderer>().material;
        objectLight.SetColor("_EmissionColor", bright);
    }
    
    
    public override void OnLaserInput(LaserHit hit) {
        //빛에 맞으면 Goal Object를 회전 시킨다.
        turnObject.transform.Rotate(new Vector3(0, 0.5f, 0));
        particleSystem.SetActive(true);

        currntTime += Time.deltaTime;
        if (count < 10) {
            //색이 점점 밝아지게 하고 싶다.
            if (currntTime >= 0.05f) {
                bright += new Color(0.1f, 0.1f, 0.1f);
                count++;
                currntTime = 0;
            }
        }

        objectLight.SetColor("_EmissionColor", bright);

    }



    public override void OnLaserInputEnd(LaserHit hit) {
        //빛이 사라지면  Goal Object를 회전이 멈춘다.
        particleSystem.SetActive(false);

        //색을 다시 어둡게 하고 싶다.
        bright = new Color(0, 0, 0);
        objectLight.SetColor("_EmissionColor", bright);

        currntTime = 0;
        count = 0;
    }
}
