using UnityEngine;
using System.Collections;
using Vuforia;

public class GyroTest : MonoBehaviour {

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cametaCintainer;
    private Quaternion rot;

    private void Start()
    {
        VuforiaBehaviour.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaBehaviour.Instance.RegisterOnPauseCallback(OnPaused); // 카메라 초점모드

        
        cametaCintainer = new GameObject("Camera Cintainer");
        cametaCintainer.transform.position = transform.position;
        transform.SetParent(cametaCintainer.transform);
        //Input.gyro.updateInterval = 0.01f;
        gyroEnabled = EnableGyro();
    }
    private void OnVuforiaStarted() // 카메라
    {
        bool focusModeSet = CameraDevice.Instance.SetFocusMode(
     CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        if (!focusModeSet)
        {
            Debug.Log("Failed to set focus mode (unsupported mode).");
        }
    }

    private void OnPaused(bool paused) // 카메라
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
    private bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cametaCintainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }

    private void Update()
    {
        if(gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }
}
