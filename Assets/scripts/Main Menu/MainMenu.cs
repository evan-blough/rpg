using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SceneManager.instance.StartCoroutine(SceneManager.instance.TransitionTime("Enter_Scene", .5f));
    }

    public void OnGameLoad()
    {
        return;
    }

    public void OnNewGame()
    {
        SceneManager.instance.StartCoroutine(SceneManager.instance.EnterGame());
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
