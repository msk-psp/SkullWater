using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public AndroidJavaObject and;

    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void End()
    {
        Application.Quit();
    }
    void Start()
    {
        AndroidJNI.AttachCurrentThread();
        AndroidJavaClass AJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        and = AJC.GetStatic<AndroidJavaObject>("currentActivity");
    }
    void Update()
    {
        and.Call("ToastMessage");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
             Application.Quit();
        }
    }
}
