using UnityEngine;
using UnityEngine.UI;

public class ArmorButton : MonoBehaviour
{
    public Armor armor;
    public Text text;

    public void PopulateButton(Armor newArmor)
    {
        armor = newArmor;
        text.text = armor is null ? "None" : armor.name;
    }
}
