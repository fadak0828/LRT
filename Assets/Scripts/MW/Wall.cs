using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, StageEnv {
    public float downSpeed = 0.1f;
    public GameObject dustParticle;
    public int finishCount=50;
    int count;

    private void Start() {
        dustParticle.SetActive(false);
    }
    public void OnStageClear() {
        dustParticle.SetActive(true);

        Invoke("DownWall", 0.1f);
        

    }

    void DownWall() {
        if (count == finishCount) {
            CancelInvoke("DownWall");
            gameObject.SetActive(false);
            dustParticle.SetActive(false);
            count = 0;
        }
        gameObject.transform.position += new Vector3(0, -downSpeed, 0);

        count++;
        Invoke("DownWall", 0.1f);

    }


}
