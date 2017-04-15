using UnityEngine;
using Firebase.Auth;
using Firebase.Unity.Editor;
using System.Collections;

public class FirebaseAnonymouslyLoginBehaviour : MonoBehaviour {
    const string PLAYERID = "USER_ID_KEY";
    const string FIREBASE_URL = "https://unity-d5c83.firebaseio.com/";
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetString(PLAYERID).Length <= 0)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;

            auth.SignInAnonymouslyAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                    {
                        FirebaseUser newUser = task.Result;
                        PlayerPrefs.SetString(PLAYERID, newUser.UserId);
                        PlayerPrefs.Save();
                    }

                });
        }
        Debug.Log("saved id:" + PlayerPrefs.GetString(PLAYERID));
	}

}
