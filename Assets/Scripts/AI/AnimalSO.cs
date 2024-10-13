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
    [Space]
    public float attackDamage = 10f;
    public float babySpeed = 3.5f;
    public float Speed = 3.5f;
    public float runSpeed = 4.5f;
    public bool isDefensiveOnHit = false;
    public ulong ticksTillMate = 1000;
    public ulong ticksTillDeath = 100000;
    [Space]
    public ulong ScorePerTick = 1;
    [Space]
    public AnimalSO[] favouriteFood;
}