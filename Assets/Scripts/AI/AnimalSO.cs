using UnityEngine;

[CreateAssetMenu(fileName = "EmptyAnimal", menuName = "AI/New Animal")]
public class AnimalSO : ScriptableObject
{
    public string AnimalName;
    public GameObject AnimalVisual;
    [TextArea] public string AnimalDesc;
    public Sprite AnimalIcon;
    public FactionSO AnimalFaction;
    [Header("Shop Settings")]
    public ulong AnimalCost;
    public ulong UnlockCost;
    [Header("Animal Stats")]
    public float Health = 100;
    public float Energy = 100;
    [Header("Hunt Settings")]
    [Range(0, 1)] public float energyToHunt = .6f; // From 0 - 1
    [Range(0, 1)] public float energyToEat = .6f; // From 0 - 1
    public bool isDefensiveOnHit = false;
    [Space]
    public float attackDamage = 10f;
    public float babySpeed = 3.5f;
    public float Speed = 3.5f;
    public float runSpeed = 4.5f;
    public ulong ticksTillMate = 1000;
    public ulong ticksTillDeath = 100000;
    [Space]
    public ulong ScorePerTick = 1;
    [Space]
    public AnimalSO[] favouriteFood;
}