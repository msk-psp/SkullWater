using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    private RawImage Toast;
    private Color Tcolor,Tecolor,FTcolor,FTecolor; // Toastcolor,Textcolor(First)
    private Text ToastText;

	// Use this for initialization
	void Start () {
        Toast = gameObject.GetComponent<RawImage>();
        ToastText = gameObject.transform.GetChild(0).GetComponent<Text>(); // 토스트 글씨
        Toast.color = new Color(0.433f, 0.769f, 1.0f, 0.518f); // 토스트 패널 색
        ReturnColor();
        FTcolor = Tcolor; // 처음 음영 다시 받아오기 위한 설정
        FTecolor = Tecolor;
	}
	
	// Update is called once per frame
	void Update () {
		//if(this.gameObject.activeInHierarchy)
        //{
            Tcolor.a -= Time.deltaTime*0.35f; // 알파값 감소
            Toast.color = Tcolor;
            Tecolor.a -= Time.deltaTime * 0.7f;
            ToastText.color = Tecolor;
            if (Toast.color.a <= 0.0f)
            {
                gameObject.SetActive(false);
                Toast.color = FTcolor;
                ToastText.color = FTecolor;
                ReturnColor();
            }
        //}
    }
    void ReturnColor()
    {
        Tcolor = Toast.color; // 토스트 패널 색
        Tecolor = ToastText.color; // 글씨색
    }
}
