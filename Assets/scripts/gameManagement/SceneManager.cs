using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public CameraHandler cam;
    public EnemyHandler enemyInBattle;
    public Animator animator;
    Scene overworld;

    public IEnumerator StartBattle(EnemyHandler enemy)
    {
        enemyInBattle = enemy;

        ControlsManager.OverworldToBattle();

        overworld = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        foreach (var thing in overworld.GetRootGameObjects())
        {
            thing.SetActive(false);
        }

        yield return StartCoroutine(TransitionTime("Exit_Scene", .5f));

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
    }

    public IEnumerator TransitionFromBattle(List<PlayerCharacter> playerCharacterList, bool battleWon)
    {
        GameManager.instance.partyManager.SetPartyData(playerCharacterList);

        ControlsManager.BattleToOverworld();

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        yield return StartCoroutine(TransitionTime("Exit_Scene", .5f));

        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Battle");


        foreach (var thing in overworld.GetRootGameObjects())
        {
            thing.SetActive(true);
        }
        if (battleWon) Destroy(enemyInBattle.gameObject);

        yield return StartCoroutine(TransitionTime("Enter_Scene", .5f));

    }

    public IEnumerator EnterGame()
    {
        yield return StartCoroutine(TransitionTime("Exit_Scene", 1f));

        UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld");

        yield return StartCoroutine(TransitionTime("Enter_Scene", 1f));
    }

    public IEnumerator OpenMenu()
    {
        overworld = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        GameManager.instance.controlsManager.OverworldToMenu();

        foreach (var thing in overworld.GetRootGameObjects())
        {
            thing.SetActive(false);
        }

        yield return StartCoroutine(TransitionTime("Exit_Scene", .5f));

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }

    public IEnumerator CloseMenu()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Menu");

        GameManager.instance.controlsManager.MenuToOverworld();

        foreach (var thing in overworld.GetRootGameObjects())
        {
            thing.SetActive(true);
        }

        yield return StartCoroutine(TransitionTime("Enter_Scene", .5f));
    }

    public IEnumerator ReturnToMainMenu()
    {
        yield return StartCoroutine(TransitionTime("Exit_Scene", .5f));

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        yield return StartCoroutine(TransitionTime("Enter_Scene", .5f));
    }

    public IEnumerator TransitionTime(string trigger, float time)
    {
        animator.SetTrigger(trigger);

        yield return new WaitForSeconds(time);

        animator.ResetTrigger(trigger);
    }

    public void SaveGame()
    {
        return;
    }
}
