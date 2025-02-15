using System;
using UnityEngine;

public class StatusMenuElements : MonoBehaviour
{
    public GameObject weaknessBox;
    public GameObject resistBox;
    public GameObject immuneBox;
    public GameObject elemTextPrefab;
    public void PopulateData(PlayerCharacterData pcd)
    {
        foreach (Transform child in weaknessBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in resistBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in immuneBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Elements element in Enum.GetValues(typeof(Elements)))
        {
            if (element == Elements.None) continue;

            var temp = Instantiate(elemTextPrefab, weaknessBox.transform);
            temp.GetComponent<ElementText>().PopulateText(element, pcd.elemWeaknesses.Contains(element));
            temp = Instantiate(elemTextPrefab, resistBox.transform);
            temp.GetComponent<ElementText>().PopulateText(element, pcd.elemResistances.Contains(element));
            temp = Instantiate(elemTextPrefab, immuneBox.transform);
            temp.GetComponent<ElementText>().PopulateText(element, pcd.elemImmunities.Contains(element));
        }
    }

}
