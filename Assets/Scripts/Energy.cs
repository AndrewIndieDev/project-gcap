using UnityEngine;

public class Energy : MonoBehaviour, IEdible
{
    public float CurrentEnergy => energy;
    public float MaxEnergy => maxEnergy;

    float IEdible.Energy { get => energy; set => energy = value; }
    float energy = 1000;
    float maxEnergy = 1000;

    public void Init(float max, float current = 0)
    {
        energy = current == 0 ? max : current;
        maxEnergy = max;

        EnableTick();
    }

    public void SetCurrentEnergy(float amount) => energy = amount;

    public void EnableTick() => TickSystem.onTick += OnTick;
    public void DisableTick() => TickSystem.onTick -= OnTick;

    public void Modify(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, maxEnergy);
    }

    private void OnTick(int tickIndex)
    {
        if (energy > 0)
            energy -= 1;
    }
}
