using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject mainDisplay;
    public GameObject itemDisplay;
    public GameObject equipMenu;
    public GameObject skillsMenu;
    public GameObject classesMenu;
    public GameObject statusMenu;

    void Start()
    {
        mainDisplay.gameObject.SetActive(true);
        CloseAllUnusedTabs(mainDisplay.transform);
    }

    public void OpenItemMenu()
    {
        itemDisplay.GetComponentInChildren<CharacterItemDisplay>().RefreshCharView();
        itemDisplay.GetComponentInChildren<InventoryDisplayHandler>().PopulateFieldMenu();
        itemDisplay.SetActive(true);
        CloseAllUnusedTabs(itemDisplay.transform);
    }

    public void OpenMainDisplay()
    {
        mainDisplay.GetComponentInChildren<MenuData>().FillSlotData();
        mainDisplay.SetActive(true);
        CloseAllUnusedTabs(mainDisplay.transform);
    }
    public void OpenEquipMenu(PlayerCharacterData pcd)
    {
        equipMenu.GetComponentInChildren<EquipMenuData>().PopulateData(pcd);
        equipMenu.gameObject.SetActive(true);
        CloseAllUnusedTabs(equipMenu.transform);
    }

    public void OpenSkillsMenu(PlayerCharacterData pcd)
    {
        skillsMenu.GetComponentInChildren<SkillsMenuData>().PopulateData(pcd);
        skillsMenu.gameObject.SetActive(true);
        CloseAllUnusedTabs(skillsMenu.transform);
    }

    public void OpenClassMenu(PlayerCharacterData pcd)
    {
        classesMenu.GetComponentInChildren<ClassMenuData>().PopulateData(pcd);
        classesMenu.gameObject.SetActive(true);
        CloseAllUnusedTabs(classesMenu.transform);
    }

    public void OpenStatusMenu(PlayerCharacterData pcd)
    {
        statusMenu.GetComponentInChildren<StatusMenuData>().PrepareData(pcd);
        statusMenu.gameObject.SetActive(true);
        CloseAllUnusedTabs(statusMenu.transform);
    }

    public void PrepareTargets(Action controls, Action<PlayerCharacterData> openMenu)
    {
        foreach (CharacterDisplay display in mainDisplay.GetComponentsInChildren<CharacterDisplay>())
        {
            var button = display.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => controls());
            button.onClick.AddListener(() => openMenu(display.pcd));
            button.interactable = true;
        }
    }

    public void PrepareFormationTargets(Action controls, Action<PlayerCharacterData> swap2)
    {
        foreach (CharacterDisplay display in mainDisplay.GetComponentsInChildren<CharacterDisplay>())
        {
            var button = display.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            if (display.pcd.isActive)
            {
                button.onClick.AddListener(() => controls());
                button.onClick.AddListener(() => swap2(display.pcd));
                button.interactable = true;
            }
        }
    }

    public void CancelTargets()
    {
        foreach (CharacterDisplay display in mainDisplay.GetComponentsInChildren<CharacterDisplay>())
        {
            var button = display.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.interactable = false;
        }
    }

    public void PrepareFormationSwap(PlayerCharacterData pcd, Action<PlayerCharacterData, PlayerCharacterData> swap)
    {
        foreach (CharacterDisplay display in mainDisplay.GetComponentsInChildren<CharacterDisplay>())
        {
            var button = display.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            if (display.pcd != pcd && pcd.isActive)
            {
                button.onClick.AddListener(() => swap(display.pcd, pcd));
                button.interactable = true;
            }
            else
                button.interactable = false;
        }
    }

    public void CheckIfMainIsOpen()
    {
        if (mainDisplay.activeInHierarchy) return;

        mainDisplay.GetComponentInChildren<MenuData>().FillSlotData();
        mainDisplay.SetActive(true);
        CloseAllUnusedTabs(mainDisplay.transform);
    }

    public void CloseSkillModal()
    {
        skillsMenu.GetComponentInChildren<SkillUseModal>().gameObject.SetActive(false);
    }

    public void CloseAllUnusedTabs(Transform openTab)
    {
        foreach (Transform child in this.transform)
        {
            if (child != openTab)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
