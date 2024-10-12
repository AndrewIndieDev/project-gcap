using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    private TapGestureRecognizer tapGesture;

    private void Start()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapGestureCallback;
        FingersScript.Instance.AddGesture(tapGesture);
    }

    //for touch. Follow transform logic will have to be redone. Perhaps do a overlap sphere and follow that way. this logic should not exist in this script
    private void TapGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            //CameraController.Instance.SetFollowTransform(transform);
        }
    }

    public void OnMouseDown()
    {
        CameraController.Instance.SetFollowTransform(transform);
    }
}
