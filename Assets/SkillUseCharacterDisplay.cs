using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseCharacterDisplay : MonoBehaviour
{
    public Image portrait;
    public Text hpView;
    public Text spView;
    public Text unitName;
    public Text level;
    public Text status;
    public PlayerCharacterData pcd;

    public void CreateCharacterDisplay(PlayerCharacterData data)
    {
        pcd = data;
        portrait.sprite = pcd.portrait;
        PopulateCharacterDisplay();
    }

    public void PopulateCharacterDisplay()
    {
        level.text = $"Level {pcd.level}";
        unitName.text = pcd.unitName;
        string statusText = pcd.currStatuses.Any() ? pcd.currStatuses.First().status.ToString() : string.Empty;
        status.text = statusText;
        hpView.text = $"HP: {pcd.currHP} / {pcd.maxHP}";
        spView.text = $"SP: {pcd.currSP} / {pcd.maxSP}";
    }
}
