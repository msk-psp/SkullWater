using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
public class loadScene : MonoBehaviour {

    public DateTime EndNow = DateTime.Now;
    public int EndStatus = 0;
    private AndroidJavaObject javaObj = null;
    AndroidJavaObject activity;


    // Use this for initialization
    void Start () {
        AndroidJavaClass jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jclass.GetStatic<AndroidJavaObject>("currentActivity");
    }
	
	// Update is called once per frame
	void Update () {
        DateTime now = DateTime.Now;//시간 계속 갱신
        TimeSpan datediff = now - EndNow;

     //   GetJavaObject().Call("setActivity", activity);
        if (EndStatus == 1 && datediff.Seconds == 2)
            EndStatus = 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EndStatus == 0)
            {
                EndNow = DateTime.Now;//백키 누른 시간을 저장
                EndStatus = 1;
       //         GetJavaObject().Call("EscapeMessage");
            }
            else if (EndStatus == 1)
            {
                Application.Quit();
            }
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void End()
    {
        Application.Quit();
    }
    private AndroidJavaObject GetJavaObject()
    {
        if (javaObj == null)
        {
            javaObj = new AndroidJavaObject("com.kpu.burniture.MainActivity");
        }
        return javaObj;
    }
}
