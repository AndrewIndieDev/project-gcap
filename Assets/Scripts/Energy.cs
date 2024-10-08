using UnityEngine;

public class Energy : MonoBehaviour, IEdible
{
    float IEdible.Energy { get => energy; set => energy = value; }
    float energy = 100;
    float maxEnergy = 100;

    public void Init(float max)
    {
        energy = max;
        maxEnergy = max;
    }

    public void Modify(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, maxEnergy);
    }
}
