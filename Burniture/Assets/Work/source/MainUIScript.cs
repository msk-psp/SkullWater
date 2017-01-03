using UnityEngine;
using System.Collections;

public class MainUIScript : MonoBehaviour {

	public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
