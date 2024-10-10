using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health = 100;
    public float CurrentHealth { get { return health; } }
    float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    float IDamagable.Health { get => health; set => health = value; }

    public UnityEvent pTakeDamage;
    public UnityEvent pOnDeath;

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

    public void Damage(float amount, bool forceWakeUp = true) {
        if (health <= 0)
            return;

        if (forceWakeUp)
            pTakeDamage.Invoke();
        health = Mathf.Clamp(health - amount, 0, maxHealth);

        if (health <= 0) {
            pOnDeath.Invoke();
        }
    }
}
