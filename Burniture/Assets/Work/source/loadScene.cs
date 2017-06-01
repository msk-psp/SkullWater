using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Threading;
public class loadScene : MonoBehaviour {

    public DateTime EndNow = DateTime.Now;
    public int EndStatus = 0;
    public GameObject Toast;
    private float screenx, screeny;

    // Use this for initialization
    void Start () {
        GameObject.Find("SandTimer").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0f);   // Screenshot 객체 투명화
        Toast.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        DateTime now = DateTime.Now;//시간 계속 갱신
        TimeSpan datediff = now - EndNow;
        

        if (EndStatus == 1 && datediff.Seconds == 2)
            EndStatus = 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EndStatus == 0)
            {
                EndNow = DateTime.Now;//백키 누른 시간을 저장
                EndStatus = 1;
                Toast.SetActive(true);

                //삭제해주는코드 
                Invoke("FalseActive", 3);
            }
            else if (EndStatus == 1)
            {
                Application.Quit();
            }
        }
    }
    public void ChangeScene(string sceneName)
    {
        GameObject.Find("SandTimer").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0.7f);   // Screenshot 객체 불투명화
        SceneManager.LoadScene(sceneName);
    }
    public void FalseActive()
    {
        Toast.SetActive(false);
    }
    public void End()
    {
        Application.Quit();
    }
}
