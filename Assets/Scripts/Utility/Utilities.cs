using System;
using UnityEngine;
using UnityEngine.Windows;

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

    /// <summary>
    /// Remaps a value from one range to another.
    /// </summary>
    /// <param name="s">The variable to be remapped.</param>
    /// <param name="a1">The minimum value of the original range.</param>
    /// <param name="a2">The maximum value of the original range.</param>
    /// <param name="b1">The minimum value of the new range.</param>
    /// <param name="b2">The maximum value of the new range.</param>
    /// <returns>The remapped value.</returns>
    public static float Remap(this float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    /// <summary>
    /// Remaps a value from one range to another, clamped between the new range.
    /// </summary>
    /// <param name="s">The variable to be remapped.</param>
    /// <param name="a1">The minimum value of the original range.</param>
    /// <param name="a2">The maximum value of the original range.</param>
    /// <param name="b1">The minimum value of the new range.</param>
    /// <param name="b2">The maximum value of the new range.</param>
    /// <returns>The remapped value.</returns>
    public static float RemapClamped(this float s, float a1, float a2, float b1, float b2)
    {
        return Mathf.Clamp(Remap(s, a1, a2, b1, b2), b1, b2);
    }

    /// <summary>
    /// Opens a link in the devices default browser.
    /// </summary>
    /// <param name="url">URL to open.</param>
    public static void OpenLink(string url)
    {
        string sanitizedUrl = Uri.EscapeUriString(url);
        Application.OpenURL(sanitizedUrl);
    }
}
