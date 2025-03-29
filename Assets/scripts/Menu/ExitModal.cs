using UnityEngine;

public class ExitModal : MonoBehaviour
{
    public void ExitGame()
    {
        GameManager.instance.sceneManager.StartCoroutine(GameManager.instance.sceneManager.ReturnToMainMenu());
    }

    public void CancelExit()
    {
        gameObject.SetActive(false);
    }
}
