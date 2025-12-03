using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;

    [SerializeField]
    private AudioSource soundEffectObject;
    [SerializeField]
    private AudioClip music;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        PlaySound(music, .7f, true);
    }

    public void PlaySound(AudioClip audioClip, float volume = 1f, bool loop = false)
    {
        // instantiate sound FX prefab
        AudioSource audioSource = Instantiate(soundEffectObject, Camera.main.transform.position, Quaternion.identity);

        // assign audio clip
        audioSource.clip = audioClip;

        // set volume
        audioSource.volume = volume;
        audioSource.loop = loop;

        // play sound
        audioSource.Play();

        // destroy clip after it has played
        if (!loop)
            Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}