using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{


    public void PlayGame() {
        Fader.instance.FadeOut(1);
        SceneManager.LoadScene("Background");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
