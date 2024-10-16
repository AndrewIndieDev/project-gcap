using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health = 100;
    public float CurrentHealth { get { return health; } }
    float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    float IDamagable.Health { get => health; set => health = value; }

    public UnityEvent pTakeDamage;
    public UnityEvent pOnDeath;

    private AIBase lastAttacker;
    public AIBase LastAttacker { get { return lastAttacker; } set { lastAttacker = LastAttacker; } }

    // Start is called before the first frame update
    public void Init(float max)
    {
        maxHealth = max;
        health = max;

        if (pTakeDamage == null)
            pTakeDamage = new UnityEvent();

        if (pOnDeath == null)
            pOnDeath = new UnityEvent();
        
    }

    public void Damage(float amount, bool forceWakeUp = true, AIBase attacker = null) {
        if (health <= 0)
            return;

        if (attacker)
        {
            lastAttacker = attacker;
            //Debug.Log($"{lastAttacker.animal.AnimalName} has attacked me!");
        }

        if (forceWakeUp)
            pTakeDamage.Invoke();
        health = Mathf.Clamp(health - amount, 0, maxHealth);

        if (health <= 0) {
            pOnDeath.Invoke();
        }
    }

    public void Damage(float amount, AIBase attacker = null)
    {
        Damage(amount, true, attacker);
    }
    /*
    public void Damage(float amount, bool forceWakeUp = true, AIBase attacker = null)
    {
        if (health <= 0)
            return;

        lastAttacker = attacker;
        Debug.Log($"{lastAttacker.animal.AnimalName} has attacked me!");

        if (forceWakeUp)
            pTakeDamage.Invoke();
        health = Mathf.Clamp(health - amount, 0, maxHealth);

        if (health <= 0)
        {
            pOnDeath.Invoke();
        }
    }
    */
}
