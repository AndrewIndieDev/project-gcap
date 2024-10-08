using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEdible
{
    float Energy { get; set; }
    void Modify(float amount);
}
