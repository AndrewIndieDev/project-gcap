using TMPro;
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

    public StartingSO settings;
    public GameObject animalPrefab;
    public GameObject cameraRig;

    [Header("temp UI")]
    public TMP_Text timeScaleText;

    private void Start()
    {
        GameObject go = null;
        for (int i = 0; i < settings.startingTrees; i++)
        {
            go = Instantiate(animalPrefab);
            go.transform.position = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
        }

        for (int i = 0; i < settings.startingAnimals.Length; i++)
        {
            go = Instantiate(animalPrefab);
            go.transform.position = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
            go.GetComponent<AIBase>().animal = settings.startingAnimals[i];
        }

        PointSystem.AddPoints(settings.startingPoints);
    }

    public void ChangeSpeed(int speed)
    {
        Time.timeScale = speed;
        timeScaleText.text = "x" + speed.ToString();
    }
}