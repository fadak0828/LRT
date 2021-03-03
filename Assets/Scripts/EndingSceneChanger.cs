using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneChanger : MonoBehaviour
{

    public void MainGame()
    {
        SceneManager.LoadScene("StartScene");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
