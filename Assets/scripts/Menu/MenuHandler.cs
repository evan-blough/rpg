using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject mainDisplay;
    public GameObject itemDisplay;

    void Start()
    {
        mainDisplay.SetActive(true);
        itemDisplay.SetActive(false);
    }

    public void OpenItemMenu()
    {
        itemDisplay.GetComponentInChildren<InventoryDisplayHandler>().PopulateFieldMenu();
        itemDisplay.SetActive(true);
        mainDisplay.SetActive(false);
    }

    public void OpenMainDisplay()
    {
        mainDisplay.GetComponentInChildren<MenuData>().FillSlotData();
        mainDisplay.SetActive(true);
        itemDisplay.SetActive(false);
    }
}
