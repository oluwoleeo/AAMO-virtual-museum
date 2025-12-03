using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;

    [SerializeField]
    private AudioSource soundEffectObject;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip audioClip, Transform transform, float volume = 1f)
    {
        // instantiate sound FX prefab
        AudioSource audioSource = Instantiate(soundEffectObject, transform.position, Quaternion.identity);

        // assign audio clip
        audioSource.clip = audioClip;

        // set volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // destroy clip after it has played
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
