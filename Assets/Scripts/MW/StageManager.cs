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
    
    void Update()
    {
        bool allGoalIn = needGoals.Select(g => g.goalIn).All(g => g);
        if (allGoalIn) {
            foreach(GameObject se in environments) {
                se.GetComponent<StageEnv>().OnStageClear();
            }
            this.enabled = false;
        }
    }
}
