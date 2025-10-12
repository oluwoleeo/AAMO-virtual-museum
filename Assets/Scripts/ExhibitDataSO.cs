using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExhibitDataSO", order = 1)]
public class ExhibitDataSO : ScriptableObject
{
    public string exhibitName;
    [TextArea(15, 20)]
    public string description;
    public Sprite[] images;
}
