using UnityEngine;

public class UISpeedButtons : MonoBehaviour
{
    public void SetSpeed(int speed)
    {
        TimeManager.ChangeSpeed(speed);
    }
}
