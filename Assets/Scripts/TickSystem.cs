using System.Collections;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public delegate void Tick();
    public static Tick onTick;

    public static ulong CurrentTick;
    public static int TicksPerSecond = 60;
    public static float TickTime => 1f / TicksPerSecond;

    private void Start()
    {
        StartCoroutine(OnTickEnumerator());
    }

    private IEnumerator OnTickEnumerator()
    {
        while (true)
        {
            CurrentTick++;
            onTick?.Invoke();
            yield return new WaitForSeconds(TickTime);
        }
    }
}
