using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioListener listener;
    public AudioSource music;

    public void ChangeAudio(AudioClip song)
    {
        music.clip = song;
        music.Play();
    }
}
