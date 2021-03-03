using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneChanger : MonoBehaviour
{

    public void MainGame()
    {
        SceneManager.LoadScene("Background");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
