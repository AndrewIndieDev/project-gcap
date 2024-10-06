using UnityEngine;

[CreateAssetMenu(fileName = "EmptyAnimal", menuName = "AI/New Animal")]
public class AnimalSO : ScriptableObject
{
    public string animalName;
    [TextArea] public string animalDesc;
    [Space]
    public FactionSO animalFaction;
}