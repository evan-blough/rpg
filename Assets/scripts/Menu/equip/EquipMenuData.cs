using UnityEngine;
using UnityEngine.UI;

public class EquipMenuData : MonoBehaviour
{
    public CharacterEquipmentDisplay equipDisplay;
    public EquipmentItemData equipItemData;
    public StatDiffBlock statDiffBlock;
    public ElemDiffBlock elemDiffBlock;
    public Text heroName;
    public Text heroWeightClass;
    PlayerCharacterData data;

    public void PopulateData(PlayerCharacterData pcd)
    {
        data = pcd;
        heroName.text = data.unitName;
        heroWeightClass.text = $"Weight Class: {data.weightClass.ToString()}";
        equipDisplay.PopulateView(data, this);
        statDiffBlock.PopulateBlock(data);
        elemDiffBlock.PopulateBlock(data);
        equipItemData.PopulateData(data, this);
    }

    public void WeaponTargets(Weapon weapon)
    {
        equipItemData.PopulateWeapons();
    }

    public void ArmorTargets(Armor armor)
    {
        equipItemData.PopulateArmor();
    }

    public void Accessory1Targets(Accessory accessory)
    {
        equipItemData.PopulateAccessories(accessory);
    }

    public void Accessory2Targets(Accessory accessory)
    {
        equipItemData.PopulateAccessories(accessory);
    }

    public void UpdateMenu()
    {
        equipDisplay.UpdateView(this);
        statDiffBlock.PopulateBlock(data);
    }
}
