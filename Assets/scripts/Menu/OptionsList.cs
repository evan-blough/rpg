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

        controls.menu.Return.performed += ReturnToOverworld;
    }

    public void OnItemsButton()
    {
        menuHandler.OpenItemMenu();
        ActivateSubmenuControls();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void ReturnToOverworld(InputAction.CallbackContext ctx)
    {
        SceneManager.instance.StartCoroutine(SceneManager.instance.CloseMenu());
    }

    public void ReturnToMainStatusMenu(InputAction.CallbackContext ctx)
    {
        menuHandler.OpenMainDisplay();
        controls.menu.Return.performed -= ReturnToMainStatusMenu;
        controls.menu.Return.performed += ReturnToOverworld;
    }

    public void ActivateSubmenuControls()
    {
        controls.menu.Return.performed -= ReturnToOverworld;
        controls.menu.Return.performed += ReturnToMainStatusMenu;
    }
}
