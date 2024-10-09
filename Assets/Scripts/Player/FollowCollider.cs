using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    public void OnMouseDown()
    {
        CameraController.instance.SetFollowTransform(transform);
    }
}
