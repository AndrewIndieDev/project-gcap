using AndrewDowsett.CommonObservers;
using System.Collections;
using UnityEngine;

public class TickSystem : MonoBehaviour, IUpdateObserver
{
    public delegate void Tick(int tickIndex);
    public static Tick onTick;

    public static bool startTicking = false;
    public static ulong CurrentTick;
    public static int TicksPerSecond = 10;
    public static float TimeBetweenTicks => 1f / TicksPerSecond;
    public static float TimeBetweenTicksForDinos => TimeBetweenTicks * UpdateAnimalsEveryXTicks;
    public static int UpdateAnimalsEveryXTicks = 4;
    public static int CurrentTickIndex;

    private int currentTickIndex = 0;
    private bool isRunning = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        UpdateManager.RegisterObserver(this);
    }

    public void ObservedUpdate(float deltaTime)
    {
        if (!isRunning && startTicking)
        {
            StartTicking();
        }
    }

    public void StartTicking()
    {
        ResetGame();
        StartCoroutine(OnTickEnumerator());
    }

    private void ResetGame()
    {
        startTicking = false;
        onTick = null;
        CurrentTick = 0;
        CurrentTickIndex = 0;
        isRunning = false;
    }

    private IEnumerator OnTickEnumerator()
    {
        yield return new WaitForSeconds(0.1f);
        isRunning = true;
        while (isRunning)
        {
            startTicking = false;
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
