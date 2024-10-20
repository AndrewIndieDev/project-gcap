using UnityEngine;

public class StatBarEnabler : MonoBehaviour
{
    public float statSizeScale = 10f;

    public void Start()
    {
        transform.localScale = Vector3.one * statSizeScale;
        DontDestroyOnLoad(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.GetComponent<UIHealthAndEnergy>() != null)
        {
            if(other.GetComponent<UIHealthAndEnergy>().Canvas != null)
                other.GetComponent<UIHealthAndEnergy>().Canvas.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<UIHealthAndEnergy>() != null)
        {
            if (other.GetComponent<UIHealthAndEnergy>().Canvas != null)
                other.GetComponent<UIHealthAndEnergy>().Canvas.SetActive(false);
        }
    }
}
