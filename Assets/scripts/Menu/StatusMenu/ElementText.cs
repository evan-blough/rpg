using UnityEngine;
using UnityEngine.UI;

public class ElementText : MonoBehaviour
{
    public Text text;
    public Elements element;

    public void PopulateText(Elements newElement, bool isActive)
    {
        element = newElement;
        text.text = element.ToString();
        text.color = isActive ? Color.white : Color.gray;
    }
}
