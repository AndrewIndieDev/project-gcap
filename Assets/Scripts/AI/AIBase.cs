using UnityEngine;

public class AIBase : MonoBehaviour
{
    public AnimalSO animal;
    public AINavigation Navigation;
    public Transform visualRoot;
    public Animator anim;

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
    }

    public void ChangeAnimation(EAnimRef animation)
    {
        anim.SetInteger("State", (int)animation);
        anim.SetBool("Reset", true);
    }
}
