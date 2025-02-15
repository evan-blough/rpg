using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    Weapon weapon;
    public Text text;

    public void PopulateButton(Weapon newWeapon)
    {
        weapon = newWeapon;
        text.text = weapon is null ? "None" : weapon.name;
    }
}
