using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEdible
{
    public float Energy { get; set; }
    public void Modify(float amount);
}
