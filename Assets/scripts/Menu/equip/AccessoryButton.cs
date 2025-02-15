using UnityEngine;
using UnityEngine.UI;
public class AccessoryButton : MonoBehaviour
{
    public Accessory accessory;
    public Text text;
    public void PopulateButton(Accessory newAccessory)
    {
        accessory = newAccessory;
        text.text = accessory is null ? "None" : accessory.name;
    }
}
