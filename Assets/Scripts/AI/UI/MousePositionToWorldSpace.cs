using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionToWorldSpace : MonoBehaviour
{
    public LayerMask hitMask;
    
    void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        Vector3 worldPosition = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitMask))
        {
            worldPosition = hitData.point;
        }

        transform.position = worldPosition;
    }
}
