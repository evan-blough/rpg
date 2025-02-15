using UnityEngine;

public class EquipMenuData : MonoBehaviour
{
    public CharacterEquipmentDisplay equipDisplay;
    public EquipmentItemData equipItemData;
    public StatDiffBlock statDiffBlock;
    PlayerCharacterData data;

    public void PopulateData(PlayerCharacterData pcd)
    {
        data = pcd;
        equipDisplay.PopulateView(data, this);
        statDiffBlock.PopulateBlock(data);
        equipItemData.PopulateData(data, this);
    }

    public void WeaponTargets(Weapon weapon)
    {
        statDiffBlock.PopulateBlock(data, weapon);
        equipItemData.PopulateWeapons();
    }

    public void ArmorTargets(Armor armor)
    {
        statDiffBlock.PopulateBlock(data, armor);
        equipItemData.PopulateArmor();
    }

    public void Accessory1Targets(Accessory accessory)
    {
        statDiffBlock.PopulateAccessory1Block(data, accessory);
        equipItemData.PopulateAccessories(accessory);
    }

    public void Accessory2Targets(Accessory accessory)
    {
        statDiffBlock.PopulateAccessory2Block(data, accessory);
        equipItemData.PopulateAccessories(accessory);
    }

    public void UpdateMenu()
    {
        equipDisplay.UpdateView(this);
        statDiffBlock.PopulateBlock(data);
    }
}
