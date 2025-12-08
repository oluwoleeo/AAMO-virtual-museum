using TMPro;
using UnityEngine;

public class ExhibitInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] AudioSource audioSource;
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
