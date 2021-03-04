using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoResizeText : MonoBehaviour
{
    private Text textBox;
    public RectTransform background;
    private string _text;
    public string text {
        get { return _text; }
        set {
            if (textBox == null) {
                textBox = GetComponent<Text>();
            }
            _text = value;

            TextGenerator textGen = new TextGenerator();
            TextGenerationSettings generationSettings = textBox.GetGenerationSettings(new Vector2(1000, 45));
            
            int padding = (value != null && value.Length > 0) ? 10 : 0;

            float width = textGen.GetPreferredWidth(value, generationSettings) + padding;
            float height = textGen.GetPreferredHeight(value, generationSettings) + padding;

            textBox.text = text;

            if (width < 1000) {
                background.sizeDelta = new Vector2(width, height);
            } else {
                background.sizeDelta = new Vector2(1000, height);
            }
        }
    }
}
