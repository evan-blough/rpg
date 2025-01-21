using UnityEngine;

public class ExitModal : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.instance.StartCoroutine(SceneManager.instance.ReturnToMainMenu());
    }

    public void CancelExit()
    {
        gameObject.SetActive(false);
    }
}
