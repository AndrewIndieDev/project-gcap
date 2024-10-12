using UnityEngine;

[CreateAssetMenu(fileName = "new StartingSO", menuName = "StartingSO")]
public class SettingsSO : ScriptableObject
{
    [Header("Starting Stats")]
    public ulong startingPoints;
    public int startingTrees;

    [Header("Starting Lists")]
    public AnimalSO[] startingAnimals;
}
