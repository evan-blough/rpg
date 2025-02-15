using UnityEngine;
using UnityEngine.UI;

public class ElemDiffBlock : MonoBehaviour
{
    public Text header;
    public Text absorptionText;
    public Text absorptionElemsText;
    public Text resistanceText;
    public Text resistanceElemsText;
    public Text weaknessText;
    public Text weaknessElemText;
    PlayerCharacterData data;

    public void PopulateBlock(PlayerCharacterData pcd)
    {
        data = pcd;
        PopulateArmorBlock();
    }

    public void PopulateWeaponBlock()
    {
        string tempString = string.Empty;

        header.text = "Weapon Affinities";
        absorptionText.text = "Elemental Damage Type";
        absorptionElemsText.text = data.weapon is not null ? data.weapon.element.ToString() : "None";

        resistanceText.text = "Statuses Inflicted";
        if (data.weapon is not null)
        {
            foreach (Statuses status in data.weapon.statuses)
                tempString += status.status.ToString() + " ";
        }
        resistanceElemsText.text = tempString == string.Empty ? "None" : tempString;

        weaknessText.text = string.Empty;
        weaknessElemText.text = string.Empty;
    }
    public void PopulateArmorBlock()
    {
        string tempString = string.Empty;

        header.text = "Armor Elemental Affinities";
        absorptionText.text = "Elemental Immunities";
        foreach (Elements element in data.elemImmunities)
            tempString += element.ToString() + " ";
        absorptionElemsText.text = tempString == string.Empty ? "None" : tempString;

        tempString = string.Empty;

        resistanceText.text = "Elemental Resistances";
        foreach (Elements element in data.elemResistances)
            tempString += element.ToString() + " ";
        resistanceElemsText.text = tempString == string.Empty ? "None" : tempString;

        tempString = string.Empty;

        weaknessText.text = "Elemental Weaknesses";
        foreach (Elements element in data.elemWeaknesses)
            tempString += element.ToString() + " ";
        weaknessElemText.text = tempString == string.Empty ? "None" : tempString;
    }

    public void PopulateArmorBlock(Armor armor)
    {
        string tempString = string.Empty;

        header.text = "Arnor Elemental Affinities";
        absorptionText.text = "Elemental Immunities";
        foreach (Elements element in armor.elemAbsorption)
            tempString += element.ToString() + " ";
        absorptionElemsText.text = tempString == string.Empty ? "None" : tempString;

        tempString = string.Empty;

        resistanceText.text = "Elemental Resistances";
        foreach (Elements element in armor.elemResists)
            tempString += element.ToString() + " ";
        resistanceElemsText.text = tempString == string.Empty ? "None" : tempString;

        tempString = string.Empty;

        weaknessText.text = "Elemental Weaknesses";
        foreach (Elements element in armor.elemWeaknesses)
            tempString += element.ToString() + " ";
        weaknessElemText.text = tempString == string.Empty ? "None" : tempString;
    }

    public void PopulateAccessoryBlock()
    {
        string tempString = string.Empty;

        header.text = "Accessory Status Resistances";
        absorptionText.text = "Status Immunities";
        foreach (Status status in data.immunities)
            tempString += status.ToString() + " ";
        resistanceText.text = "Status Resistances";
        // resisted statuses here
        weaknessText.text = "";
        weaknessElemText.text = string.Empty;
    }
}
