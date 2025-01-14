using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneManager sceneManager;

    private void Start()
    {
        sceneManager.StartCoroutine(SceneManager.instance.TransitionTime("Enter_Scene", .5f));
    }

    public void OnGameLoad()
    {
        return;
    }

    public void OnNewGame()
    {
        sceneManager.StartCoroutine(sceneManager.EnterGame());
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
