using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static ulong Points => points;
    private static ulong points = 1800;
    public static bool AddPoints(ulong amount)
    {
        if (points + amount <= ulong.MaxValue)
        {
            points += amount;
            return true;
        }
        points = ulong.MaxValue;
        return true;
    }
    public static bool RemovePoints(ulong amount)
    {
        if (points >= amount)
        {
            points -= amount;
            return true;
        }
        return false;
    }
    public static new string ToString() => points.ToString("N0");
}
