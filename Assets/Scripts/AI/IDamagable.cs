using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float Health { get; set; }
    //void Damage(float amount, bool forceWakeUp = true);
    void Damage(float amount, bool forceWakeUp = true, AIBase attacker = null);
    void Damage(float amount, AIBase attacker = null);
}
