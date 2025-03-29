using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.sceneManager.StartCoroutine(GameManager.instance.sceneManager.TransitionTime("Enter_Scene", .5f));
    }

    public void OnGameLoad()
    {
        return;
    }

    public void OnNewGame()
    {
        GameManager.instance.sceneManager.StartCoroutine(GameManager.instance.sceneManager.EnterGame());
    }

    public void OnOptions()
    {
        return;
    }

    public void OnQuitOut()
    {
        Application.Quit();
    }
}
