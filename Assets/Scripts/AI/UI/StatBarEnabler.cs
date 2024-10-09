using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBarEnabler : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.GetComponent<UIHealthAndEnergy>() != null)
        {
            other.GetComponent<UIHealthAndEnergy>().Canvas.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<UIHealthAndEnergy>() != null)
        {
            other.GetComponent<UIHealthAndEnergy>().Canvas.SetActive(false);
        }
    }
}
