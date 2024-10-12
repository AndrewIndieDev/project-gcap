using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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

    public SettingsSO settings;
    public GameObject animalPrefab;
    public GameObject cameraRig;

    [Header("temp UI")]
    public TMP_Text timeScaleText;

    private void Start()
    {
        GameObject go = null;
        Vector3 position = Vector3.zero;

        for (int i = 0; i < settings.startingTrees; i++)
        {
            while (position == Vector3.zero)
                position = GetRandomPositionAroundTarget(Vector3.zero, 45f);

            go = Instantiate(animalPrefab);
            go.transform.position = GetRandomPositionAroundTarget(position, 1f);
            go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
            
            position = Vector3.zero;
        }

        for (int i = 0; i < settings.startingAnimals.Length; i++)
        {
            while (position == Vector3.zero)
                position = GetRandomPositionAroundTarget(Vector3.zero, 45f);

            go = Instantiate(animalPrefab);
            go.transform.position = position;
            go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
            go.GetComponent<AIBase>().animal = settings.startingAnimals[i];

            position = Vector3.zero;
        }

        PointSystem.AddPoints(settings.startingPoints);
    }

    public void ChangeSpeed(int speed)
    {
        Time.timeScale = speed;
        timeScaleText.text = "x" + speed.ToString();
    }

    public Vector3 GetRandomPositionAroundTarget(Vector3 targetPosition, float radius)
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius; // Get a random point in a circle
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + targetPosition; // Adjust for 3D space

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 100f, NavMesh.AllAreas))
        {
            if (hit.position.y >= 4.99f)
                return hit.position;
        }

        return Vector3.zero;
    }
}