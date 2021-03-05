using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneChanger : MonoBehaviour
{
    void SceneChange()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void MainGame()
    {
        Fader.instance.FadeOut(1, SceneChange);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
