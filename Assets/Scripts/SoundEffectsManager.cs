using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;

    [SerializeField]
    private AudioSource soundEffectObject;
    [SerializeField]
    private AudioClip music;
    [SerializeField] float musicVolume = 0.25f;
    [SerializeField]
    private AudioMixerGroup audioMixerGroup;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        PlaySound(music, musicVolume, true);
    }

    public void PlaySound(AudioClip audioClip, float volume = 1f, bool loop = false)
    {
        // instantiate sound FX prefab
        AudioSource audioSource = Instantiate(soundEffectObject, Camera.main.transform.position, Quaternion.identity);
        audioSource.transform.parent = Camera.main.transform;

        // assign audio clip
        audioSource.clip = audioClip;

        // set volume
        audioSource.volume = volume;
        audioSource.loop = loop;

        if (loop)
            audioSource.outputAudioMixerGroup = audioMixerGroup;

        // play sound
        audioSource.Play();

        // destroy clip after it has played
        if (!loop)
            Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}