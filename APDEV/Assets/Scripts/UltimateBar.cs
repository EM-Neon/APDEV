using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateBar : MonoBehaviour
{
    public Slider slider;
    public void SetUltimate(int ult)
    {
        slider.value = ult;
    }

    
}
