using UnityEngine;

public class AIBase : MonoBehaviour
{
    public AnimalSO animal;
    public AINavigation Navigation;
    public Transform visualRoot;
    public Animator anim;
    public AIRangeSensor rangeSensor;
    public Health health;

    public void Start()
    {
        InitializeAnimal();
    }

    public void InitializeAnimal()
    {
        if (!animal)
            return;

        Navigation.agent.speed = animal.Speed;
        GameObject go = Instantiate(animal.AnimalVisual, visualRoot);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        anim = go.GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    public void ChangeAnimation(EAnimRef animation)
    {
        anim.SetInteger("State", (int)animation);
        anim.SetBool("Reset", true);
    }

    public bool CheckForPredators()
    {
        if (!rangeSensor)
            return false;

        if(rangeSensor.ClosestPredator != null)
            return true;
        
        return false;
    }

    public bool CheckForPrey()
    {
        if (!rangeSensor)
            return false;

        if (rangeSensor.ClosestPrey != null)
            return true;

        return false;
    }
}
