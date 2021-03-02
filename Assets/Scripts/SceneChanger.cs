using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void PlayGame() {

        SceneManager.LoadScene("Background");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
