using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour {
    public string sName;
	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(sName);
    }
}