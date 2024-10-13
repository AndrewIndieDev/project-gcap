using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void ChangeSpeed(int speed)
    {
        Time.timeScale = speed;
    }
}
