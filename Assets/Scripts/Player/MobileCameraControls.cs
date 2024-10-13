using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

[RequireComponent(typeof(CameraController))]
public class MobileCameraControls : MonoBehaviour
{
    private PanGestureRecognizer panGesture;
    private RotateGestureRecognizer rotateGesture;
    private ScaleGestureRecognizer scaleGesture;

    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            return;
        
        HandleRotateGesture();
        HandleScaleGesture();
        HandlePanGesture();

        panGesture.AllowSimultaneousExecution(scaleGesture);
        panGesture.AllowSimultaneousExecution(rotateGesture);
        scaleGesture.AllowSimultaneousExecution(rotateGesture);
    }

    private void ScaleGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            //DebugText("Scaled: {0}, Focus: {1}, {2}", scaleGesture.ScaleMultiplier, scaleGesture.FocusX, scaleGesture.FocusY);
            //CameraController.Instance.zoomProgress *= (scaleGesture.ScaleMultiplierRange + 1)/2;
            CameraController.Instance.zoomProgress -= (scaleGesture.ScaleDistanceDelta / 10f);
        }
    }

    private void HandleScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private void PanGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            // Calculate the movement deltas based on the pan gesture
            float deltaX = panGesture.DeltaX / 25.0f;
            float deltaY = panGesture.DeltaY / 25.0f;

            // Get the forward and right directions of the camera (horizontal plane)
            Vector3 forward = CameraController.Instance.transform.forward;
            Vector3 right = CameraController.Instance.transform.right;

            // Ensure movement only affects the XZ plane (ignores the Y component)
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Calculate the relative movement based on input
            Vector3 movement = (-right * deltaX) + (-forward * deltaY);

            // Update the camera's position
            Vector3 pos = CameraController.Instance.newPosition;
            pos += movement;
            CameraController.Instance.newPosition = pos;
        }
    }

    private void HandlePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MaximumNumberOfTouchesToTrack = 2;
        panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }

    private void RotateGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            CameraController.Instance.newRotation *= Quaternion.Euler(new Vector3(0.0f, rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg * 2.5f, 0f));
        }
    }

    private void HandleRotateGesture()
    {
        rotateGesture = new RotateGestureRecognizer();
        rotateGesture.StateUpdated += RotateGestureCallback;
        FingersScript.Instance.AddGesture(rotateGesture);
    }
}
