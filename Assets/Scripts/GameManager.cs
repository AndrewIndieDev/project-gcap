using UnityEngine;

public enum EAnimRef
{
    IDLE,
    WALK = 12
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
