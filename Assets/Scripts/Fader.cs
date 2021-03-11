using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    public static Fader instance;

    private void Awake() {
        instance = this;
    }

    public Image imageFader;
    float fadeOutTime;
    System.Action fadeOutCallback;
    // 점점 어두워 지는 기능을 만들고 싶다
    public void FadeOut(float time, System.Action callback = null) {
        this.fadeOutTime = time;
        this.fadeOutCallback = callback;
        StartCoroutine("ieFadeOut");
    }

    IEnumerator ieFadeOut() {
        float add = Mathf.Min(Time.deltaTime, 1 / 144f);
        Color c = imageFader.color;
        for (float a = 0; a < fadeOutTime; a += add) {
            c.a = a / fadeOutTime;
            imageFader.color = c;
            yield return new WaitForSeconds(add);
        }
        c.a = 1;
        imageFader.color = c;
        if (fadeOutCallback != null) {
            fadeOutCallback();
        }
    }

    float fadeinTime;
    System.Action fadeinCallback;
    // 점점 밝아지는 기능을 만들고 싶다
    public void Fadein(float time, System.Action callback = null) {
        this.fadeinTime = time;
        this.fadeinCallback = callback;
        StartCoroutine("ieFadein");
    }

    IEnumerator ieFadein() {
        float add = Mathf.Min(Time.deltaTime,  1 / 144f);
       
        Color c = imageFader.color;
        for (float a = 0; a < fadeinTime; a += add) {
            c.a = 1 - (a / fadeinTime);
            imageFader.color = c;
            yield return new WaitForSeconds(add);
        }
        c.a = 0;
        imageFader.color = c;
        if (fadeinCallback != null) {
            fadeinCallback();
        }
    }

    void Start() {

    }


    void Update() {

    }
}
