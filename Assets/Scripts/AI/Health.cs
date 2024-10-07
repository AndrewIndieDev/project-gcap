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
    [SerializeField] protected float healthRegenBuffer = 2f;
    [SerializeField] protected float healthRegenSpeed = 0.1f;
    float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    float IDamagable.Health { get => health; set => health = value; }

    public UnityEvent pTakeDamage;
    public UnityEvent pOnDeath;

    float elapsedTime = 0f;
    [SerializeField] bool regenerateHealth = true;
    float regenElapsedTime, regenDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        if (pTakeDamage == null)
            pTakeDamage = new UnityEvent();

        if (pOnDeath == null)
            pOnDeath = new UnityEvent();
        
    }

    void Update() {
        if (health >= maxHealth || !regenerateHealth)
        {
            elapsedTime = 0f;
            return;
        }

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= healthRegenBuffer)
        {
            health = Mathf.Lerp(health, maxHealth, Time.deltaTime * healthRegenSpeed);
            health = Mathf.Clamp(health, 0, maxHealth);

            if (health == maxHealth || (maxHealth - health) <= 0.98f)
            {
                elapsedTime = 0;
            }
        }


    }

    public void Damage(float amount) {
        elapsedTime = 0f;
        pTakeDamage.Invoke();
        health = Mathf.Clamp(health - amount, 0, maxHealth);

        if (health <= 0) {
            pOnDeath.Invoke();
        }
    }
}
