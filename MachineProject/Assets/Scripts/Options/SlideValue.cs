using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlideValue : MonoBehaviour
{
    public Slider slide;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = slide.value.ToString();
    }

    public void onSliderChanged()
    {
        text.text = slide.value.ToString();
    }
}
