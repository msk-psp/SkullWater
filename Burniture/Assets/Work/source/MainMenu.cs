using UnityEngine;
using System.Collections;
<<<<<<< HEAD
using UnityEngine.UI;
=======
>>>>>>> newStopsBranch
using System;

public class MainMenu : MonoBehaviour {

    public DateTime EndNow = DateTime.Now;
    public int EndStatus = 0;

    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void End()
    {
        Application.Quit();
    }
#if UNITY_ANDROID
    private AndroidJavaObject javaObj = null;

    private AndroidJavaObject GetJavaObject()
    {
        if (javaObj == null)
        {
            javaObj = new AndroidJavaObject("com.kpu.burniture.MainActivity");
        }
        return javaObj;
    }

    void Update()
    {
        DateTime now = DateTime.Now;//시간 계속 갱신
        TimeSpan datediff = now - EndNow;
        // Retrieve current Android Activity from the Unity Player
        AndroidJavaClass jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jclass.GetStatic<AndroidJavaObject>("currentActivity");

        // Pass reference to the current Activity into the native plugin,
        // using the 'setActivity' method that we defined in the ImageTargetLogger Java class
        GetJavaObject().Call("setActivity", activity);
        if (EndStatus == 1 && datediff.Seconds == 2)
            EndStatus = 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EndStatus == 0)
            {
                EndNow = DateTime.Now;//백키 누른 시간을 저장
                EndStatus = 1;
                GetJavaObject().Call("EscapeMessage");
            }
            else if (EndStatus == 1)
            {
                Application.Quit();
            }
        }
        
    }
#else
    private void ShowTargetInfo(string targetName, float targetWidth, float targetHeight) {
        Debug.Log("ShowTargetInfo method placeholder for Play Mode (not running on Android device)");
    }
#endif
}
