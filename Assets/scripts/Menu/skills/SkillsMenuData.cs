using UnityEngine;

public class SkillsMenuData : MonoBehaviour
{
    PlayerCharacterData pcd;
    public SkillDisplayHandler skillDisplay;
    public SkillsCharacterHolder characterHolder;
    public void PopulateData(PlayerCharacterData data)
    {
        pcd = data;
        skillDisplay.PopulateUseMenu(pcd);
        characterHolder.CreateCharacterDisplay(pcd);
    }
}
