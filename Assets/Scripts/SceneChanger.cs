using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class SceneChanger : MonoBehaviour
{
    public GameObject bgBorder;
    public void PlayGame() {
        Material mat = bgBorder.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", mat.color * 0f);
        //bgBorder.SetActive(false);
        Fader.instance.FadeOut(1, () => NextScene());

        //SceneManager.LoadScene("Background");
    }
    public void QuitGame() {
        Application.Quit();
    }

    

    void NextScene() {
        SceneManager.LoadScene("Background");
    }
}
