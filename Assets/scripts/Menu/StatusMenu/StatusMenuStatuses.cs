using System;
using System.Linq;
using UnityEngine;

public class StatusMenuStatuses : MonoBehaviour
{
    public GameObject currentBox;
    public GameObject resistBox;
    public GameObject immuneBox;
    public GameObject statusTextPrefab;
    public void PopulateData(PlayerCharacterData pcd)
    {
        foreach (Transform child in currentBox.transform)
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

        foreach (Status status in Enum.GetValues(typeof(Status)))
        {
            var temp = Instantiate(statusTextPrefab, currentBox.transform);
            temp.GetComponent<StatusText>().PopulateText(status, pcd.currStatuses.Any(c => c.status == status));
            temp = Instantiate(statusTextPrefab, resistBox.transform);
            temp.GetComponent<StatusText>().PopulateText(status, pcd.resistances.Contains(status));
            temp = Instantiate(statusTextPrefab, immuneBox.transform);
            temp.GetComponent<StatusText>().PopulateText(status, pcd.immunities.Contains(status));
        }
    }
}
