/*********************
* 프로그램 명 : 컬러 픽커 버튼
* 작성자 : 정지훈
* 작성일자 : 20170330
*********************/
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ColorPick : MonoBehaviour
{
    bool stateSwitch;                                   // 스위치의 상태를 알기 위한 bool 변수
    public int windowWidth;                             // 캔버스의 Width를 저장 하기 위한 변수
    public int windowHeight;                            // 캔버스의 Height를 저장 하기 위한 변수
    private bool takeHiResShot = false;
    bool touchState;                                    // 터치 상태 변수
    int temp_cullingMask;                               // 카메라의 초기 cullingMask값을 저장 하기 위한 변수
    Texture2D tempImage;                                // 임시로 Texture2D 형식의 이미지를 저장할 변수
    Texture2D screenShot;                               // 스크린샷을 Texture2D형식으로 저장하기 위한 변수
    CameraClearFlags temp_clearFlags;                   // 카메라의 초기 clearFlags값을 저장 하기 위한 변수
    public Canvas myScreen;                             // 나의 캔버스 정보를 받아오기 위한 클래스 변수
    public GameObject Spuit;                            // Spuit 이미지를 저장하기 위한 변수
    public Color selectColor;                           // 선택한 색
    GameObject test;
    const string COLORS = "COLORS";

    // Use this for initialization
    void Start()
    {
        touchState = false;                             // 시작할 때, touchState변수 false로 초기화
        stateSwitch = false;                            // 시작할 때, stateSwitch변수 false로 초기화
        temp_cullingMask = Camera.main.cullingMask;     // 초기 cullingMask 임시저장
        temp_clearFlags = Camera.main.clearFlags;       // 초기 clearFlags 임시저장
        windowHeight = (int)myScreen.pixelRect.height;  // 나의 캔버스의 height 저장
        windowWidth = (int)myScreen.pixelRect.width;    // 나의 캔버스의 width 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (stateSwitch && Input.touchCount == 1)        // switch가 켜지고, 누르고 있을 때
        {
            selectColor = screenShot.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);                      // 스크린샷의 좌표에 해당 색 추출
            Spuit.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);                       // Spuit 이미지 좌표 변경
            GameObject.Find("ColorPicker").GetComponent<RawImage>().color = selectColor;                                    // 추출된 컬러 삽입
            GameObject.Find("Spuit").GetComponent<RawImage>().color = selectColor;                                          // 추출된 컬러 삽입
            PlayerPrefs.SetString(COLORS, string.Format("{0},{1},{2}", selectColor.r, selectColor.g, selectColor.b));
            for (int i = 1; i < 9; i++)                                                                                     // Sphere의 색도 변경.
            {
                GameObject.Find("Sphere" + i).GetComponent<Renderer>().material.color = selectColor;
            }
            touchState = true;
        }
        else if (Input.touchCount == 0 && touchState)         // 누른 것이 없고, 터치 변수가 true이면
        {
            GameObject.Find("Screenshot").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0f); // Screenshot 객체 투명화
            GameObject.Find("ScreenEdge").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0f); // Screenshot 객체 투명화
            GameObject.Find("Spuit").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0f);      // Spuit 객체 투명화
            Camera.main.clearFlags = temp_clearFlags;           // 카메라 기능 ON
            Camera.main.cullingMask = temp_cullingMask;         // 카메라 기능 ON
            stateSwitch = false;                                // switch OFF
            touchState = false;                                 // off
        }
    }

    public void SwitchOn()                              // swith를 눌렀을 때
    {
        MyCapture();                                    // Capture를 한다
        Camera.main.clearFlags = 0;                     // 화면을 멈춘다
        Camera.main.cullingMask = 0;                    // 화면을 멈춘다
        GameObject.Find("Screenshot").GetComponent<RawImage>().texture = tempImage;                         // tempImage를 Screenshot 객체에 덮어 씌운다.
        GameObject.Find("Screenshot").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 1f);     // Screenshot 객체 불투명화
        GameObject.Find("ScreenEdge").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0.7f);   // Screenshot 객체 불투명화
        GameObject.Find("Spuit").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0.7f);        // Spuit 객체 불투명화
        Invoke("ChangeStateValue", 1.0f);               //1초 뒤 상태변수 변경
    }
    void ChangeStateValue()                             // 상태변수 변경.
    {
        stateSwitch = true;
    }
    void MyCapture()                                    // 카메라 캡쳐 부분 tempImage에 스크린샷을 저장한다.
    {
        Rect mcap = new Rect(0, 0, windowWidth, windowHeight);
        RenderTexture rt = new RenderTexture(windowWidth, windowHeight, 24);
        Camera.main.targetTexture = rt;
        screenShot = new Texture2D(windowWidth, windowHeight,
            TextureFormat.RGB24, false);
        tempImage = new Texture2D(2, 2);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(mcap, 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();                                // screenShot을 PNG 형태로 인코딩

        tempImage.LoadImage(bytes);
        takeHiResShot = false;
    }
}