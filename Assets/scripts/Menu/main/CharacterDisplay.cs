using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public Image portrait;
    public Text level;
    public Text nameText;
    public Text className;
    public Text health;
    public Text skillPoints;
    public Text status;
    public PlayerCharacterData pcd;
    public void SetDisplayData()
    {
        portrait.sprite = pcd.portrait;
        level.text = pcd.level.ToString();
        string statusText = pcd.currStatuses.Any() ? pcd.currStatuses.First().status.ToString() : string.Empty;
        status.text = statusText;
        nameText.text = pcd.unitName;
        health.text = $"{pcd.currHP} / {pcd.maxHP}";
        skillPoints.text = $"{pcd.currSP} / {pcd.maxHP}";
    }
}
