using UnityEngine;
using System.Collections;
using Vuforia;

public class Gyro : MonoBehaviour {
    
    // Use this for initialization
    void Start()
    {
        VuforiaBehaviour.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaBehaviour.Instance.RegisterOnPauseCallback(OnPaused); // 카메라 초점모드
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.01f;
    }
    private void OnVuforiaStarted()
    {
        bool focusModeSet = CameraDevice.Instance.SetFocusMode(
     CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        if (!focusModeSet)
        {
            Debug.Log("Failed to set focus mode (unsupported mode).");
        }
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

    void Update()
    {
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x,
                          -Input.gyro.rotationRateUnbiased.y,
                          0);
    }

    /*private Gyroscope gyro;
    private bool gyrosupport;
    private Quaternion rotfix;

    [SerializeField]
    private Transform Worldobj;
    private float startY;

    // Use this for initialization
    void Start()
    {
        gyrosupport = SystemInfo.supportsGyroscope;
        GameObject camParent = new GameObject("camParent");
        camParent.transform.position = transform.position;
        transform.parent = camParent.transform;

        if (gyrosupport)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            rotfix = new Quaternion(0, 0, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gyrosupport && startY == 0)
        {
            ResetGyroRotation();
        }
        transform.rotation = gyro.attitude * rotfix;
    }

    void ResetGyroRotation()
    {
        startY = transform.eulerAngles.y;
        Worldobj.rotation = Quaternion.Euler(0f, startY, 0f);
    }*/
}
