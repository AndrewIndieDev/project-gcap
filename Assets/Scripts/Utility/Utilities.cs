using UnityEngine;

public class Utilities
{
    public static bool Buffer(ref float timer, float duration)
    {
        timer += TickSystem.TickTime;
        //Debug.Log($"[Buffer] Waited {timer} out of {duration}.");
        if (timer > duration)
        {
            timer = 0;
            return true;
        }

        return false;
    }
}
