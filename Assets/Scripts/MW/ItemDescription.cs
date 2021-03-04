using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public static bool isShowed;
    [TextArea]
    public string description;
    public float allShowTime = 1;
    public float duration = 3;
    public void Show() {
        if (isShowed == false) {
            SubTitle.Instance.Show(description, allShowTime, duration);
            isShowed = true;
        }
    }
}
