using UnityEngine;
using UnityEngine.UI;
public class CharacterHUD : MonoBehaviour
{
    public Text nameText;
    public Text hpText;
    public Text spText;
    public Character character;
    public void SetHUD()
    {
        if (character.currHP == 0)
        {
            nameText.color = Color.red;
            hpText.color = Color.red;
            spText.color = Color.red;
        }
        else if (character.currHP <= character.maxHP * .33)
        {
            nameText.color = Color.yellow;
            hpText.color = Color.yellow;
            spText.color = Color.yellow;
        }
        else
        {
            nameText.color = Color.white;
            hpText.color = Color.white;
            spText.color = Color.white;
        }

        nameText.text = character.unitName;
        hpText.text = character.currHP.ToString() + "/" + character.maxHP.ToString();
        spText.text = character.currSP.ToString();
    }
}
