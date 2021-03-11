using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTitle : MonoBehaviour
{
    public static SubTitle Instance;
    public AutoResizeText textBox;
    private void Awake() {
        Instance = this;    
    }

    public void Show(string sub, float allShowTime, float duration) {
        StartCoroutine(StartSub(sub, allShowTime, duration));
    }

    private IEnumerator StartSub(string sub, float allShowTime, float duration) {
        int endIndex = 0;
        while(endIndex < sub.Length) {
            endIndex++;
            textBox.text = sub.Substring(0, endIndex);
            yield return new WaitForSeconds(allShowTime / sub.Length);
        }
        yield return new WaitForSeconds(duration);
        textBox.text = "";
    }
}
