using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsList : MonoBehaviour
{
    PlayerControls controls;
    public MenuHandler menuHandler;

    private void Awake()
    {
        controls = ControlsHandler.instance.playerControls;
    }
    private void Start()
    {
        SceneManager.instance.TransitionTime("Enter_Scene", .5f);

        controls.menu.Return.started += ReturnToOverworld;
    }

    public void OnItemsButton()
    {
        menuHandler.OpenItemMenu();
        ActivateSubmenuControls();
    }
    public void OnEquipButton()
    {
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenEquipMenu);
        ActivateCharacterSelectionControls();
    }
    public void OnSkillsButton()
    {
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenSkillsMenu);
        ActivateCharacterSelectionControls();
    }
    public void OnClassesButton()
    {
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenClassMenu);
        ActivateCharacterSelectionControls();
    }

    public void OnStatusButton()
    {
        menuHandler.PrepareTargets(ActivateSubmenuControls, menuHandler.OpenStatusMenu);
        ActivateCharacterSelectionControls();
    }

    public void OnExitButton()
    {
        SceneManager.instance.StartCoroutine(SceneManager.instance.ReturnToMainMenu());
    }

    public void ReturnToOverworld(InputAction.CallbackContext ctx)
    {
        controls.menu.Return.started -= ReturnToOverworld;
        SceneManager.instance.StartCoroutine(SceneManager.instance.CloseMenu());
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
