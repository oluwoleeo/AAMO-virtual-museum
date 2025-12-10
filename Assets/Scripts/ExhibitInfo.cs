using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Slider audioSlider;
    [SerializeField] AudioSource audioSource;

    void Update()
    {
        // Update the audio slider based on audio playback
        if (audioSource.clip != null)
        {
            audioSlider.value = audioSource.time / audioSource.clip.length;
        }
    }
    public void SetText(ExhibitDataSO data)
    {
        if (data == null)
            return;

        titleText.text = data.exhibitName;
        descriptionText.text = data.description;
        audioSource.clip = data.audio;
    }

    public void RewindAudio()
    {
        if (audioSource.clip == null)
            return;

        audioSource.time -= 10f;
        if (audioSource.time < 0f)
            audioSource.time = 0f;
    }
    public void ForwardAudio()
    {
        if (audioSource.clip == null)
            return;

        audioSource.time += 10f;
        if (audioSource.time > audioSource.clip.length)
            audioSource.time = 0f;
    }
}
