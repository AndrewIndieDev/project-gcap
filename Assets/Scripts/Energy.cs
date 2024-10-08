using UnityEngine;

public class Energy : MonoBehaviour, IEdible
{
    public float CurrentEnergy => energy;
    public float MaxEnergy => maxEnergy;

    float IEdible.Energy { get => energy; set => energy = value; }
    float energy = 1000;
    float maxEnergy = 1000;

    public void Init(float max)
    {
        energy = max;
        maxEnergy = max;

        TickSystem.onTick += OnTick;
    }

    public void Modify(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, maxEnergy);
    }

    private void OnTick(int tickIndex)
    {
        
    }
}
