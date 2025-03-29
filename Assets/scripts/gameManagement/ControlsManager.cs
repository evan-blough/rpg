using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Update()
    {
        if (playerControls is null) playerControls = new PlayerControls();
    }

    public static void BattleToOverworld()
    {

    }

    public static void OverworldToBattle()
    {

    }

    public void MenuToOverworld()
    {
        playerControls.menu.Disable();
        playerControls.overworld.Enable();
    }
    public void OverworldToMenu()
    {
        playerControls.overworld.Disable();
        playerControls.menu.Enable();
    }
}
