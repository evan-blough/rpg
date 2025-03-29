using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsList : MonoBehaviour
{
    PlayerControls controls;
    public MenuHandler menuHandler;
    public Button formationButton;
    public ExitModal exitModal;

    private void Awake()
    {
        controls = GameManager.instance.controlsManager.playerControls;
    }
    private void Start()
    {
        GameManager.instance.sceneManager.TransitionTime("Enter_Scene", .5f);

        controls.menu.Return.started += ReturnToOverworld;
        exitModal.gameObject.SetActive(false);

        if (GameManager.instance.partyManager.partyData.Count <= 1)
            formationButton.interactable = false;
    }

    public void OnItemsButton()
    {
        menuHandler.OpenItemMenu();
        ActivateSubmenuControls();
    }
    public void OnEquipButton()
    {
        menuHandler.CheckIfMainIsOpen();
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenEquipMenu);
        ActivateCharacterSelectionControls();
    }
    public void OnSkillsButton()
    {
        menuHandler.CheckIfMainIsOpen();
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenSkillsMenu);
        ActivateCharacterSelectionControls();
    }
    public void OnClassesButton()
    {
        menuHandler.CheckIfMainIsOpen();
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenClassMenu);
        ActivateCharacterSelectionControls();
    }

    public void OnStatusButton()
    {
        menuHandler.CheckIfMainIsOpen();
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenStatusMenu);
        ActivateCharacterSelectionControls();
    }

    public void OnFormationButton()
    {
        menuHandler.CheckIfMainIsOpen();
        menuHandler.PrepareFormationTargets(ActivateSubmenuControls, PickFormationSwap);
        ActivateCharacterSelectionControls();
    }

    public void PickFormationSwap(PlayerCharacterData pcd)
    {
        menuHandler.PrepareFormationSwap(pcd, SwapFormation);
    }

    public void SwapFormation(PlayerCharacterData pcd1, PlayerCharacterData pcd2)
    {
        List<PlayerCharacterData> battleParty = GameManager.instance.partyManager.partyData;
        if (!battleParty.Contains(pcd1) && !battleParty.Contains(pcd2)) return;

        var indexOf2 = battleParty.IndexOf(pcd2);
        battleParty[battleParty.IndexOf(pcd1)] = pcd2;
        battleParty[indexOf2] = pcd1;

        for (int i = 0; i < battleParty.Count; i++)
        {
            if (i == 2) battleParty[i].isBackRow = true;
            else battleParty[i].isBackRow = false;
        }

        menuHandler.OpenMainDisplay();
        ReturnToMainStatusMenu(new InputAction.CallbackContext { });
    }

    public void OnExitButton()
    {
        exitModal.gameObject.SetActive(true);
    }

    public void ReturnToOverworld(InputAction.CallbackContext ctx)
    {
        controls.menu.Return.started -= ReturnToOverworld;
        GameManager.instance.sceneManager.StartCoroutine(GameManager.instance.sceneManager.CloseMenu());
    }

    public void ReturnToOptionsList(InputAction.CallbackContext ctx)
    {
        menuHandler.CancelTargets();
        controls.menu.Return.started -= ReturnToOptionsList;
        controls.menu.Return.started += ReturnToOverworld;
    }

    public void ReturnToMainStatusMenu(InputAction.CallbackContext ctx)
    {
        menuHandler.OpenMainDisplay();
        controls.menu.Return.started -= ReturnToMainStatusMenu;
        controls.menu.Return.started += ReturnToOverworld;
    }

    public void ActivateSubmenuControls()
    {
        controls.menu.Return.started -= ReturnToOverworld;
        controls.menu.Return.started += ReturnToMainStatusMenu;
    }

    public void ActivateCharacterSelectionControls()
    {
        controls.menu.Return.started -= ReturnToOverworld;
        controls.menu.Return.started += ReturnToOptionsList;
    }
}
