using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PartyManager partyManager;
    public ControlsManager controlsManager;
    public AudioManager audioManager;
    public SceneManager sceneManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
