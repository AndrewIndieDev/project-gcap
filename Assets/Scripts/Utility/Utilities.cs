using UnityEngine;

public static class Utilities
{
    public static bool Buffer(ref float timer, float duration)
    {
        timer += TickSystem.TimeBetweenTicks;
        //Debug.Log($"[Buffer] Waited {timer} out of {duration}.");
        if (timer > duration)
        {
            timer = 0;
            return true;
        }

        return false;
    }

    public static bool StateBuffer(ref float timer, float duration)
    {
        timer += TickSystem.TimeBetweenTicksForDinos;
        //Debug.Log($"[Buffer] Waited {timer} out of {duration}.");
        if (timer > duration)
        {
            timer = 0;
            return true;
        }

        return false;
    }

    public static float Remap(this float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static float RemapClamped(this float s, float a1, float a2, float b1, float b2)
    {
        return Mathf.Clamp(Remap(s, a1, a2, b1, b2), b1, b2);
    }
}
