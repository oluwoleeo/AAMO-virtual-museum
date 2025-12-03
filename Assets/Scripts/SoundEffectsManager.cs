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

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        // instantiate sound FX prefab
        AudioSource audioSource = Instantiate(soundEffectObject, Camera.main.transform.position, Quaternion.identity);
        audioSource.transform.parent = Camera.main.transform;

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
