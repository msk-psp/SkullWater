using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartBurniture : MonoBehaviour {
    public string sName;
	// Use this for initialization
	void Start () {
        Invoke("SceneChange", 0.5f);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(sName);
    }
}