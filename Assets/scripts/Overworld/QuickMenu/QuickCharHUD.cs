using UnityEngine;
using UnityEngine.UI;

public class QuickCharHUD : MonoBehaviour
{
    public Text levelText;
    public Text nameText;
    public Text hpText;
    public Text spText;
    public void SetHUD(PlayerCharacterData character)
    {
        levelText.text = "Lv. " + character.level.ToString();
        nameText.text = character.unitName;
        hpText.text = character.currHP.ToString() + " / " + character.maxHP.ToString();
        spText.text = character.currSP.ToString() + " / " + character.maxSP.ToString();
    }
}
