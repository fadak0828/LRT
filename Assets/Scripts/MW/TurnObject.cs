using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObject : LaserInput {
    public GameObject turnObject;
    public GameObject particleSystem;

    private void Start() {
        particleSystem.SetActive(false);
    }

    public override void OnLaserInput(LaserHit hit) {
        //빛에 맞으면 Goal Object를 회전 시킨다.
        turnObject.transform.Rotate(new Vector3(0, 0.5f, 0));
        particleSystem.SetActive(true);
    }

    public override void OnLaserInputEnd(LaserHit hit) {
        //빛이 사라지면  Goal Object를 회전이 멈춘다.
        particleSystem.SetActive(false);

    }
}
