using UnityEngine;
using UnityEngine.InputSystem;

public class StatusMenuData : MonoBehaviour
{
    public StatusMenuStats stats;
    public StatusMenuElements elements;
    public StatusMenuStatuses statuses;
    PlayerControls controls;
    private void Awake()
    {
        stats.gameObject.SetActive(false);
        elements.gameObject.SetActive(false);
        statuses.gameObject.SetActive(false);
        controls = ControlsHandler.instance.playerControls;
    }

    public void PrepareData(PlayerCharacterData pcd)
    {
        stats.PopulateData(pcd);
        elements.PopulateData(pcd);
        statuses.PopulateData(pcd);
        stats.gameObject.SetActive(true);
    }

    public void NextPage(InputAction.CallbackContext ctx)
    {
        if (stats.gameObject.activeInHierarchy)
        {
            elements.gameObject.SetActive(true);
            stats.gameObject.SetActive(false);
        }
        else if (elements.gameObject.activeInHierarchy)
        {
            statuses.gameObject.SetActive(true);
            elements.gameObject.SetActive(false);
        }
        else if (statuses.gameObject.activeInHierarchy)
        {
            stats.gameObject.SetActive(true);
            statuses.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        controls.menu.Confirm.started += NextPage;
    }

    private void OnDisable()
    {
        controls.menu.Confirm.started -= NextPage;
        stats.gameObject.SetActive(false);
        elements.gameObject.SetActive(false);
        statuses.gameObject.SetActive(false);

    }
}
