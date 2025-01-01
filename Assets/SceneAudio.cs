using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    public AudioClip sceneMusic;

    // Start is called before the first frame update
    void Start()
    {
        PlayAudio();
    }

    private void OnEnable()
    {
        if (AudioManager.instance is not null && AudioManager.instance.music.clip != sceneMusic)
        {
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        AudioManager.instance.ChangeAudio(sceneMusic);
    }
}
