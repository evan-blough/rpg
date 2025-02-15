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
        weaponButton.PopulateButton(data.weapon);
        weaponButton.GetComponent<Button>().onClick.AddListener(() => emd.WeaponTargets(weaponButton.weapon));

        armorButton.PopulateButton(pcd.armor);
        armorButton.GetComponent<Button>().onClick.AddListener(() => emd.ArmorTargets(armorButton.armor));

        accessory1Button.PopulateButton(pcd.accessory1);
        accessory1Button.GetComponent<Button>().onClick.AddListener(() => emd.Accessory1Targets(accessory1Button.accessory));

        accessory2Button.PopulateButton(pcd.accessory2);
        accessory2Button.GetComponent<Button>().onClick.AddListener(() => emd.Accessory2Targets(accessory2Button.accessory));
    }

    public void UpdateView(EquipMenuData emd)
    {
        weaponButton.PopulateButton(data.weapon);
        weaponButton.GetComponent<Button>().onClick.RemoveAllListeners();
        weaponButton.GetComponent<Button>().onClick.AddListener(() => emd.WeaponTargets(weaponButton.weapon));

        armorButton.PopulateButton(data.armor);
        armorButton.GetComponent<Button>().onClick.RemoveAllListeners();
        armorButton.GetComponent<Button>().onClick.AddListener(() => emd.ArmorTargets(armorButton.armor));

        accessory1Button.PopulateButton(data.accessory1);
        accessory1Button.GetComponent<Button>().onClick.RemoveAllListeners();
        accessory1Button.GetComponent<Button>().onClick.AddListener(() => emd.Accessory1Targets(accessory1Button.accessory));

        accessory2Button.PopulateButton(data.accessory2);
        accessory2Button.GetComponent<Button>().onClick.RemoveAllListeners();
        accessory2Button.GetComponent<Button>().onClick.AddListener(() => emd.Accessory2Targets(accessory2Button.accessory));
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
