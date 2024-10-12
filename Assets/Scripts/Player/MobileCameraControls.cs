using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

[RequireComponent(typeof(CameraController))]
public class MobileCameraControls : MonoBehaviour
{
    private PanGestureRecognizer panGesture;
    private RotateGestureRecognizer rotateGesture;

    // Start is called before the first frame update
    void Start()
    {
        HandleRotateGesture();
    }


    private void RotateGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            CameraController.Instance.newRotation *= Quaternion.Euler(new Vector3(0.0f, rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg * 10f, 0f));
        }
    }

    private void HandleRotateGesture()
    {
        rotateGesture = new RotateGestureRecognizer();
        rotateGesture.StateUpdated += RotateGestureCallback;
        FingersScript.Instance.AddGesture(rotateGesture);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
