using UnityEngine;
using System.Collections;

public class Guide : MonoBehaviour {
    /*private AndroidJavaObject javaObj = null;

    private AndroidJavaObject GetJavaObject()
    {
        if (javaObj == null)
        {
            javaObj = new AndroidJavaObject("com.kpu.burniture.MainActivity");
        }
        return javaObj;
    }
    // Use this for initialization
    void Start () {
        AndroidJavaClass jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jclass.GetStatic<AndroidJavaObject>("currentActivity");
        GetJavaObject().Call("setActivity", activity);
        GetJavaObject().Call("Guide");
    }*/

    private Rect window = new Rect((Screen.width / 2) - 1200, (Screen.height / 2) - 500, 2400, 1000);
    private bool show = false;

    void OnGUI()
    {
        if (show)
            window = GUI.Window(0, window, DialogWindow, "Guide");
    }

    // This is the actual window.
    void DialogWindow(int windowID)
    {
        //GUI.Label(new Rect(5, y, window.width, 20), "Again?");
        GUIStyle style = GUI.skin.GetStyle("button");
        style.fontSize = 77;

        if (GUI.Button(new Rect(65, 800, window.width / 4, 150), "◀"))
        {
            show = false;
        }
        if (GUI.Button(new Rect((window.width - (window.width / 4)) / 2, 800, window.width / 4, 150), "종료"))
        {
            show = false;
        }
        if (GUI.Button(new Rect((window.width - (window.width / 4)) - 65, 800, window.width / 4, 150), "▶"))
        {
            show = false;
        }
    }

    // To open the dialogue from outside of the script.
    public void Open()
    {
        show = true;
    }
    void Start()
    {
        Open();
    }
}
