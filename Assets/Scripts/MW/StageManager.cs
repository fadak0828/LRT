using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface StageEnv {
    void OnStageClear();
}

public class StageManager : MonoBehaviour
{
    public List<Goal> needGoals;

    public List<GameObject> environments;
    public float timer = 0;

    
    private void Start() {
        
    }
    void Update()
    {
        bool allGoalIn = needGoals.Select(g => g.goalIn).All(g => g);
        if (allGoalIn) {
            timer += Time.deltaTime;
            if (timer > 1.5f) {
                //2초 뒤에 벽 내려가기
                StageClear();
                this.enabled = false;
            }
        } else {
            timer = 0;
        }
    }

    void StageClear() {
        foreach (GameObject se in environments) {
            se.GetComponent<StageEnv>().OnStageClear();
        }
        foreach(Goal goal in needGoals) {
            //ParticleSystem particle = goal.gameObject.GetComponentInChildren<ParticleSystem>();
            //particle.Stop();
            //particle.Play();

            //goal.gameObject.SetActive(false);
            goal.OnStageClear();
        }
    }
}
