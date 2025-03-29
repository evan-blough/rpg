using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    public AudioClip sceneMusic;
    bool created = false;

    // Start is called before the first frame update
    void Start()
    {
        created = true;
        PlayAudio();
    }

    private void OnEnable()
    {
        if (!created) return;

        if (GameManager.instance.audioManager is not null && GameManager.instance.audioManager.music.clip != sceneMusic)
        {
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        GameManager.instance.audioManager.ChangeAudio(sceneMusic);
    }
}
