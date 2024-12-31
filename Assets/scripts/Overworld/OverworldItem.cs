using UnityEngine;

public class OverworldItem : MonoBehaviour
{
    PlayerControls controls;
    public Items item;
    public int itemNum = 1;
    public bool isCollected = false;

    private void Start()
    {
        controls = ControlsHandler.instance.playerControls;
    }

    public void InteractWithObject()
    {
        if (!isCollected)
        {
            var text = BattlePartyHandler.instance.inventory.AddItem(item, itemNum);
            isCollected = true;
            controls.overworld.Interact.performed -= ctx => InteractWithObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacterHandler>() && !isCollected)
        {
            controls.overworld.Interact.performed += ctx => InteractWithObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacterHandler>() && !isCollected)
        {
            controls.overworld.Interact.performed -= ctx => InteractWithObject();
        }
    }
}
