using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    public void FollowTransform()
    {
        CameraController.Instance.SetFollowTransform(transform);
    }

    public void OnMouseDown()
    {
        CameraController.Instance.SetFollowTransform(transform);
    }
}
