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
    [Header("Animal Stats")]
    public double Health = 100;
    public double Energy = 100;
    public float Speed = 3.5f;
}