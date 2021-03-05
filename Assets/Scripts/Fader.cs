using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader instance;
    private void Awake()
    {
        instance = this;
    }
    public Image imageFader;
    float fadeOutTime;
    System.Action fadeOutCallback;
    // 점점 어두워지는 기능을 만들고싶다.
    public void FadeOut(float time, System.Action callback = null)
    {
        this.fadeOutTime = time;
        this.fadeOutCallback = callback;
        StartCoroutine("ieFadeOut");
    }
    IEnumerator ieFadeOut()
    {// 0->1
        float add = Time.deltaTime;
        Color c = imageFader.color;
        for (float a = 0; a < fadeOutTime; a += add)
        {
            c.a = a / fadeOutTime;
            imageFader.color = c;
            yield return new WaitForSeconds(add);
        }
        c.a = 1;
        imageFader.color = c;
        if (fadeOutCallback != null)
        {
            fadeOutCallback();
        }
    }


    float fadeInTime;
    System.Action fadeInCallback;
    // 점점 밝아지는 기능을 만들고싶다.
    public void FadeIn(float time, System.Action callback = null)
    {
        this.fadeInTime = time;
        this.fadeInCallback = callback;
        StartCoroutine("ieFadeIn");
    }

    IEnumerator ieFadeIn()
    {// 1->0
        float add = Time.deltaTime;
        Color c = imageFader.color;
        for (float a = 0; a < fadeOutTime; a += add)
        {
            c.a = 1 - (a / fadeOutTime);
            imageFader.color = c;
            yield return new WaitForSeconds(add);
        }
        c.a = 0;
        imageFader.color = c;
        if (fadeInCallback != null)
        {
            fadeInCallback();
        }
    }

}
