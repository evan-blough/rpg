using UnityEngine;
using UnityEngine.UI;
public class CharacterEquipmentDisplay : MonoBehaviour
{
    public PlayerCharacterData data;
    public Image portrait;
    public WeaponButton weaponButton;
    public ArmorButton armorButton;
    public AccessoryButton accessory1Button;
    public AccessoryButton accessory2Button;

    public void PopulateView(PlayerCharacterData pcd, EquipMenuData emd)
    {
        data = pcd;
        portrait.sprite = data.portrait;
        weaponButton.PopulateButton(pcd.weapon);
        weaponButton.GetComponent<Button>().onClick.AddListener(() => emd.WeaponTargets(pcd.weapon));

        armorButton.PopulateButton(pcd.armor);
        armorButton.GetComponent<Button>().onClick.AddListener(() => emd.ArmorTargets(pcd.armor));

        accessory1Button.PopulateButton(pcd.accessory1);
        accessory1Button.GetComponent<Button>().onClick.AddListener(() => emd.Accessory1Targets(pcd.accessory1));

        accessory2Button.PopulateButton(pcd.accessory2);
        accessory2Button.GetComponent<Button>().onClick.AddListener(() => emd.Accessory2Targets(pcd.accessory2));
    }


    public void ActivateButtons()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
            button.interactable = true;
    }

    public void DeactivateButtons()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
            button.interactable = false;
    }
}
