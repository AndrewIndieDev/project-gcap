using UnityEngine;
using UnityEngine.Events;

public enum EEvent
{
    OnGameStart
}

public class EventListener : MonoBehaviour
{
    public EEvent listenFor;
    public UnityEvent eventToTrigger;

    private void Start()
    {
        switch (listenFor)
        {
            case EEvent.OnGameStart:
                GameManager.Instance.onGameStart += EventTrigger;
                break;
            default:
                Debug.LogError("No event defined for " + listenFor);
                break;
        }
    }

    private void EventTrigger()
    {
        eventToTrigger.Invoke();
    }
}
