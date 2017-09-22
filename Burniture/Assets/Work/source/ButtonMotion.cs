using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

public class ButtonMotion : MonoBehaviour {

    public static GameObject Notify; // 알림패널
    private Text NotiText; // 패널 글씨
    private RawImage NotiRaw; // 알림 RawImage 색을 정해주기 위함
    public static  GameObject Fix, Save, Detach, Recapt; //Burniture Scene의 모션을 담을 버튼
    public static GameObject GRoom,LoadFurn; //Layout Scene의 모션을 담을 버튼
    public static int State; // 함수를 제어하기 위한 상태변수
    public static int ChangeState;
    public static float Speed;
    public static Vector3 F_position;
    private int FixButtonState;
    private int OKsign1, OKsign2, OKsign3;

    // Use this for initialization
    void Start () {
        /*모두 초기화*/
        State = 1;
        ChangeState = 1;
        Speed = 15f;
        FixButtonState = 1;
        OKsign1 = 0;
        OKsign2 = 0;
        OKsign3 = 0;

        Notify = GameObject.Find("Notify"); // 객체 등록
        NotiRaw = Notify.GetComponent<RawImage>(); // RawImage 컴포넌트 불러옴
        NotiText = Notify.transform.GetChild(0).GetComponent<Text>(); // 알림 첫번째 자식의 Text 컴포넌트 불러옴
        
        Detach = GameObject.Find("Detach"); // 마커에서 떼어내기
        Recapt = GameObject.Find("Recapt"); // 다시 촬영
        Fix = GameObject.Find("Fix"); // 교정

        if (SceneManager.GetActiveScene().name== "Burniture") // 현재 활성화된 Scene이름이 Burniture일 경우
        {
            /*Burniture Scene의 버튼 객체 등록*/
            Save = GameObject.Find("Save"); // 저장
        }
        else if (SceneManager.GetActiveScene().name== "Layout")
        {
            /*Layout Scene의 버튼 객체 등록 Recapt, Fix와 Detach는 Burniture Scene과 함께 공유*/
            GRoom = GameObject.Find("GenerateRoom");
            LoadFurn = GameObject.Find("Load");
        }

        F_position = Fix.transform.position; // 버튼의 처음위치 기억
    }
    
    // Update is called once per frame
    void Update () {
        /*상태변수에 따른 함수 실행*/
        if (SceneManager.GetActiveScene().name== "Burniture")
        {
            switch (State)
            {
                case 1:
                    BeforeMarker();
                    break;
                case 2:
                    AfterMarker();
                    break;
                case 3:
                    AfterDetatch();
                    break;
                case 4:
                    if (FixButtonState == 1)
                    {
                        AfterFix();
                    }
                    else
                    {
                        // 두번째로 노출된 Fix버튼을 누를 경우의 예외처리
                    }
                    break;
                default:
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name== "Layout")
        {
            switch (State)
            {
                case 1:
                    BeforeMarker();
                    break;
                case 2:
                    AfterMarker();
                    break;
                case 3:
                    AfterDetatch();
                    break;
                case 4:
                    if (FixButtonState == 1)
                    {
                        AfterLayoutFix();
                    }
                    else
                    {
                        // 두번째로 노출된 Fix버튼을 누를 경우의 예외처리
                    }
                    break;
                case 5:
                    AfterGenerateRoom();
                    break;
                default:
                    break;
            }
        }
    }

    public static void PositionReset()
    {
        /*버튼의 위치를 초기화 해주기 위한 함수*/
        if (SceneManager.GetActiveScene().name== "Burniture")
        {
            Save.transform.position = F_position;
        }
        else if (SceneManager.GetActiveScene().name== "Layout")
        {
            GRoom.transform.position = F_position;
            LoadFurn.transform.position = F_position;
        }

        Fix.transform.position = F_position;
        Detach.transform.position = F_position;
        Recapt.transform.position = F_position;
    }

    /*Burniture Scene의 함수*/
    private void BeforeMarker() // 마커 촬영 전
    {
        /* 버튼들 비가시화 */
        if (SceneManager.GetActiveScene().name == "Burniture")
        {
            Save.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name== "Layout")
        {
            LoadFurn.SetActive(false);
            GRoom.SetActive(false);
        }
        Fix.SetActive(false);
        Detach.SetActive(false);
        Recapt.SetActive(false);
        Notify.SetActive(true); // 마커 촬영전 알림을 띄운다.
        NotiRaw.color = new Color(0.433f, 0.769f, 1.0f, 0.518f);
        //NotiText.color = new Color(1f, (float)(64 / 255), (float)(1 / 255), 1f);
        NotiText.text = "마커를 촬영해 주세요";
    }

    public static void AfterMarker() // 마커 촬영 후 (DefaultTrackableEventHandler에서 작동)
    {
        if (ChangeState == 0)
        {
            PositionReset();
            ChangeState = 1;
        }
        Notify.SetActive(false); // 마커 인식 후 알림이 안뜸
        Detach.SetActive(true); // 버튼 가시화
    }

    private void AfterDetatch() // 떼어낸 후
    {
        if (ChangeState == 0)
        {
            PositionReset();
            ChangeState = 1;
        }
        Detach.SetActive(false);
        Recapt.SetActive(true);
        Fix.SetActive(true);

        /*버튼 2개*/
        if (Recapt.transform.position.y > Screen.height/3) // 버튼이 스크린 높이의 1/3까지 아래로 슬라이드
        {
           // Debug.Log(Screen.height / 3 + "<" + Recapt.transform.position.y + "<=" + Screen.height);
            Recapt.transform.position = new Vector3(Recapt.transform.position.x, Recapt.transform.position.y -Speed, 0);
        }
        if (Fix.transform.position.y < 2*Screen.height / 3) // 버튼이 스크린 높이의 1/3까지 위로 슬라이드
        {
            //Debug.Log(2 * Screen.height / 3 + "<" + Fix.transform.position.y);
            Fix.transform.position = new Vector3(Fix.transform.position.x, Fix.transform.position.y + Speed, 0);
        }
    }

    private void AfterFix() // 교정 후 (측정씬)
    {
        if (ChangeState == 0)
        {
            PositionReset();
            ChangeState = 1;
        }
        Recapt.SetActive(true);
        Save.SetActive(true);

        /*버튼 3개*/
        if (Save.transform.position.y < 3.5 * Screen.height / 4)
        {
            Debug.Log(Save.transform.localScale.y);
            Save.transform.position = new Vector3(Save.transform.position.x, Save.transform.position.y + Speed, 0);
        }
        else // 버튼이 이동을 마친 후 변수 변경
        {
            OKsign1 = 1;
        }
        if (Fix.transform.position.y < 2.5 * Screen.height / 4)
        {
            Fix.transform.position = new Vector3(Fix.transform.position.x, Fix.transform.position.y + Speed, 0);
        }
        else
        {
            OKsign2 = 1;
        }
        if (Recapt.transform.position.y > 1.5 * Screen.height / 4)
        {
            Recapt.transform.position = new Vector3(Recapt.transform.position.x, Recapt.transform.position.y - Speed, 0);
        }
        else
        {
            OKsign3 = 1;
        }
        if (OKsign1 == 1 && OKsign2 == 1 && OKsign3 == 1) // 모든 버튼이 제자리를 찾아갈때까지 대기
        {
            FixButtonState = 0;
        }
    }

    public void ReCapture() // 다시 촬영
    {
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start(); // 마커트래킹 start
        GameObject.Find("ARCamera").GetComponent<Gyro>().enabled = false;
        if (SceneManager.GetActiveScene().name == "Burniture")
        {
            Save.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Layout")
        {
            LoadFurn.SetActive(false);
            GRoom.SetActive(false);
        }
        Fix.SetActive(false);
        Detach.SetActive(false);
        Recapt.SetActive(false);
        Notify.SetActive(true); // 마커 촬영전 알림을 띄운다.

        State = 1; // 첫번째 단계부터 다시 시작
    }

    /*Layout Scene의 함수*/
    private void AfterLayoutFix()
    {
        if (ChangeState == 0)
        {
            PositionReset();
            ChangeState = 1;
        }
        GRoom.SetActive(true);

        if (GRoom.transform.position.y < 3.5 * Screen.height / 4)
        {
            GRoom.transform.position = new Vector3(GRoom.transform.position.x, GRoom.transform.position.y + Speed, 0);
        }
        else // 버튼이 이동을 마친 후 변수 변경
        {
            OKsign1 = 1;
        }
        if (Fix.transform.position.y < 2.5 * Screen.height / 4)
        {
            Fix.transform.position = new Vector3(Fix.transform.position.x, Fix.transform.position.y + Speed, 0);
        }
        else
        {
            OKsign2 = 1;
        }
        if (Recapt.transform.position.y > 1.5 * Screen.height / 4)
        {
            Recapt.transform.position = new Vector3(Recapt.transform.position.x, Recapt.transform.position.y - Speed, 0);
        }
        else
        {
            OKsign3 = 1;
        }
        if (OKsign1 == 1 && OKsign2 == 1 && OKsign3 == 1) // 모든 버튼이 제자리를 찾아갈때까지 대기
        {
            FixButtonState = 0;
        }
    }

    private void AfterGenerateRoom()
    {
        if (ChangeState == 0)
        {
            PositionReset();
            ChangeState = 1;
        }
        GRoom.SetActive(false);
        Fix.SetActive(false);
        Recapt.SetActive(false);
        LoadFurn.SetActive(true);
    }
    
}