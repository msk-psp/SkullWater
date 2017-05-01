using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour {
    public string sName;
	// Use this for initialization
	void Start () {
        Invoke("ChangeScene", 1.0f);               //1초 뒤 상태변수 변경
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sName);
    }
}