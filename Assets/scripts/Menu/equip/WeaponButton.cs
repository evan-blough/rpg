using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Weapon weapon;
    public Text text;

    public void PopulateButton(Weapon newWeapon)
    {
        weapon = newWeapon;
        text.text = weapon is null ? "None" : weapon.name;
    }
}
