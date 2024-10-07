using UnityEngine;

public enum EAnimRef
{
    IDLE,
    ATTACK,
    GET_HIT,
    JUMP,
    ROLL,
    TAIL_ATTACK,
    DEATH,
    GET_HIT_2,
    JUMP_FORWARD,
    PRANCING,
    SWIM,
    EAT,
    WALK,
    PRANCING_2,
    LOOK_AROUND,
    RUN,
    SLEEP,
    WAKE_UP
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject animalPrefab;
}
