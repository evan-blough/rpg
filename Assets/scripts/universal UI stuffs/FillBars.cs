using UnityEngine;
using UnityEngine.UI;

public class FillBars : MonoBehaviour
{
    public Slider slider;
    int currentValue;
    int maxValue;

    public void InitializedSlider(ref int curr, ref int max)
    {
        currentValue = curr;
        maxValue = max;
        slider.value = currentValue;
        slider.maxValue = maxValue;
    }
}
