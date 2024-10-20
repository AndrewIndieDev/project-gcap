using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void ChangeSpeed(int speed)
    {
        Time.timeScale = speed;
    }
}
