using UnityEngine;

public class QuickMenuHandler : MonoBehaviour
{
    PlayerControls controls;
    public QuickMenu menu;

    private void Start()
    {
        controls = GameManager.instance.controlsManager.playerControls;

        controls.overworld.QuickMenu.performed += ctx => ActivateQuickMenu();

        controls.overworld.Cancel.performed += ctx => DeactivateQuickMenu();

        controls.overworld.Menu.performed += ctx => OpenMenu();
    }

    public void ActivateQuickMenu()
    {
        if (menu.gameObject.activeInHierarchy)
        {
            DeactivateQuickMenu();
            return;
        }

        menu.SetMenu();

        menu.gameObject.SetActive(true);
    }

    public void DeactivateQuickMenu()
    {
        if (menu.gameObject.activeInHierarchy)
        {
            menu.gameObject.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        GameManager.instance.sceneManager.StartCoroutine(GameManager.instance.sceneManager.OpenMenu());
    }
}
