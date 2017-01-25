using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    private AndroidJavaObject and;
    private AndroidJavaClass AJC;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        AJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        and = AJC.GetStatic<AndroidJavaObject>("currentActivity");

        and.Call<bool>("ToastMessage");
    }
}
