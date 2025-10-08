using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExhibitDataSO", order = 1)]
public class ExhibitDataSO : ScriptableObject
{
    public string exhibitName;
    public string description;
    public Sprite[] images;
}
