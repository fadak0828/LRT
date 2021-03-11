using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Image bgBorder;
    public void PlayGame()
    {
        FadeOutBorder();
        Fader.instance.FadeOut(0.7f, () => {
            Invoke("NextScene", 0.5f);
        });
    }

    private void FadeOutBorder()
    {
        StartCoroutine(IeFadeOutBorder());
    }

    private IEnumerator IeFadeOutBorder()
    {
        float t = 0;
        while (t < 1)
        {
            t = Time.deltaTime;

            Material mat = bgBorder.material;
            Material fadeOutMat = new Material(Shader.Find("Standard"));
            fadeOutMat.SetColor("_EmissionColor", mat.color * (1 - t) / 1.3f);
            bgBorder.material = fadeOutMat;

            yield return new WaitForEndOfFrame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    void NextScene()
    {
        SceneManager.LoadScene("Background");
    }
}
