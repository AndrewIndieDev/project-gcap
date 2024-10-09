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

    private void Start()
    {
        GameObject go = null;
        for (int i = 0; i < 5; i++)
        {
            go = Instantiate(animalPrefab);
            go.transform.position = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
        }
    }
}