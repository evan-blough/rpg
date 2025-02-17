using UnityEngine;

public class SkillsMenuData : MonoBehaviour
{
    PlayerCharacterData pcd;
    public SkillDisplayHandler skillDisplay;
    public void PopulateData(PlayerCharacterData data)
    {
        pcd = data;
        skillDisplay.PopulateUseMenu(data);
    }
}
