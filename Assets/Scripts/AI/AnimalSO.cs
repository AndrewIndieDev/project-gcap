using UnityEngine;

[CreateAssetMenu(fileName = "EmptyAnimal", menuName = "AI/New Animal")]
public class AnimalSO : ScriptableObject
{
    public string animalName;
    [TextArea] public string animalDesc;
    public Sprite animalSprite;
    [Space]
    public FactionSO animalFaction;
    [Header("Shop Settings")]
    public ulong animalCost;
}