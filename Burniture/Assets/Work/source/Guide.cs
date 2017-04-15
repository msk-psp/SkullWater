/*********************
* 프로그램 명 : 가이드
* 작성자 : 정지훈
* 작성일자 : 20170319
*********************/

using UnityEngine;
using UnityEngine.UI;                                                       // Image를 사용하기 위해
using System.Collections;

public class Guide : MonoBehaviour
{
    public Texture myimage1;                                                // 설명서로 사용할 이미지
    public Canvas myScreen;                                                 // Canvas를 담기 위한 객체
    private Rect window;                                                    // window창 객체
    private bool show = false;
    float windowHeight;
    float windowWidth;

    // Use this for initialization
    void Start()
    {
        window = new Rect(0, 0, 2400, 1000);                                // Canvas의 크기를 가져와 Rect의 크기를 조절한다. 위치조정은 여기서 한다. (여백의 사각형 높이, 너비)
        windowHeight = myScreen.pixelRect.height;
        windowWidth = myScreen.pixelRect.width * 0.85f;
    }

    void OnGUI()                                                            // 스크립트가 활성화 될때 마다 활성화 되는 함수
    {
        GUIStyle style = GUI.skin.GetStyle("window");           // 실제 스타일 설정 부분
        style.fixedHeight = windowHeight;                       // 높이, 스크린의 100% 너비
        style.fixedWidth = windowWidth;                         // 너비, 스크린의 80% 너비
        style.fontSize = 30;
        if (show)
        {
            window = GUI.Window(0, window, DialogWindow, "");   // show 변수가 참이 될 시 해당 필드를 출력시킴.
        }
    }
    void DialogWindow(int windowID)
    {
        GUIStyle style = GUI.skin.GetStyle("button");
        GUI.backgroundColor = new Color(0f, 0f, 0f, 100f);              // 배경색
        style.fontSize = 20;                                            // 폰트 사이즈
        GUIStyle Lablestyle = GUI.skin.GetStyle("Label");
        Lablestyle.fontSize = 55;
        //test 부분입니다___
        Rect imageRect = new Rect(windowWidth * 0.1f, windowHeight * 0.2f, windowWidth * 0.8f, windowHeight * 0.7f);            // 이미지 위치와 크기정보 이미지를 담을 Rect 객체 생성
        //GUI.DrawTexture(imageRect, myimage1);
        GUI.Label(imageRect, "");

        //test
        GUI.TextField(new Rect(0, windowHeight * 0.1f, 300, 50), "test");               // 한줄
        GUI.TextArea(new Rect(0, windowHeight * 0.1f + 50, 300, 50), "aaaa");           // 여러공간

        GUI.Button(new Rect(0, 0, windowWidth * 0.7f, windowHeight * 0.1f), "도움말");
        if (GUI.Button(new Rect(windowWidth * 0.7f, 0, windowWidth * 0.1f, windowHeight * 0.1f), "◀"))
        {
            show = false;
        }
        if (GUI.Button(new Rect(windowWidth * 0.8f, 0, windowWidth * 0.1f, windowHeight * 0.1f), "▶"))
        {
            show = false;
        }
        if (GUI.Button(new Rect(windowWidth * 0.9f, 0, windowWidth * 0.1f, windowHeight * 0.1f), "X"))
        {
            show = false;
        }
    }

    // To open the dialogue from outside of the script.
    public void Open()
    {
        show = true;
    }
}
