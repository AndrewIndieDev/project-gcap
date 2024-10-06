using System.Collections;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public delegate void Tick();
    public static Tick onTick;

    public static ulong currentTick;

    public int ticksPerSecond = 60;

    private float tickTime => 1f / ticksPerSecond;

    private void Start()
    {
        StartCoroutine(OnTickEnumerator());
    }

    private IEnumerator OnTickEnumerator()
    {
        while (true)
        {
            currentTick++;
            onTick?.Invoke();
            yield return new WaitForSeconds(tickTime);
        }
    }
}
