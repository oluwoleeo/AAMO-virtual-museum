using TMPro;
using UnityEngine;

public class ExhibitInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    public void SetText(ExhibitDataSO data)
    {
        if (data == null)
            return;

        titleText.text = data.exhibitName;
        descriptionText.text = data.description;
    }
}
