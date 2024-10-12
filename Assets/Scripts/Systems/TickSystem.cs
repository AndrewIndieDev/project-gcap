using System.Collections;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public delegate void Tick(int tickIndex);
    public static Tick onTick;

    public static ulong CurrentTick;
    public static int TicksPerSecond = 10;
    public static float TimeBetweenTicks => 1f / TicksPerSecond;
    public static float TimeBetweenTicksForDinos => TimeBetweenTicks * UpdateAnimalsEveryXTicks;
    public static int UpdateAnimalsEveryXTicks = 4;
    public static int CurrentTickIndex;

    private int currentTickIndex = 0;

    private void Start()
    {
        StartCoroutine(OnTickEnumerator());
    }

    private IEnumerator OnTickEnumerator()
    {
        while (true)
        {
            CurrentTick++;
            onTick?.Invoke(currentTickIndex);
            currentTickIndex++;
            if (currentTickIndex >= UpdateAnimalsEveryXTicks)
                currentTickIndex = 0;
            CurrentTickIndex = currentTickIndex;
            yield return new WaitForSeconds(TimeBetweenTicks);
        }
    }
}
