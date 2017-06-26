using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Moving : MonoBehaviour
{

    float FirstDistance;
    public float Speed = 15f;
    private int Cube_num = 0;
    private GameObject MoveCube;
    public GameObject Furn;
    private GameObject Arrow;
    private int TouchState = 0;
    private int ChangeStatus = 1;
    private int ColorChangeState = 0;
    private Color F_Color,T_Color;
    private GameObject PreCube; // 이전 큐브 저장
    private GameObject PreArrow;
    private int start_layer;

    public Material Mat;
    void Start()
    {
        start_layer = MoveCube.layer; // 여기도
    }
    void Update()
    {
        if (Input.touchCount == 0)              // 터치가 없으면
        {
            Cube_num = 0;                     // 선택된 것도 없음
            TouchState = 0;                   // 손가락이 떼지면 변화
        }

        if (Input.touchCount == 1)              // 화면에 터치한 손가락의 갯수가 한개일때
        {
            if ((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved) && MoveCube != null)
            {
                if (ColorChangeState == 0)
                {
                    Furn = MoveCube.gameObject.transform.GetChild(1).gameObject;
                    /*try
                    {

                    }
                    catch (UnityException)
                    {
                        Debug.Log("던져!");
                    }*/
                    F_Color = Furn.GetComponent<Renderer>().material.color;
                }
                T_Color = F_Color;
                T_Color.a = 0.5f;
                Furn.GetComponent<Renderer>().material.color = T_Color;
                ColorChangeState = 1;
                Arrow= MoveCube.gameObject.transform.GetChild(0).gameObject;
                Arrow.layer = 2; // 레이캐스트 무시
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (ColorChangeState == 1)
                {
                    Furn = MoveCube.gameObject.transform.GetChild(1).gameObject;
                    Furn.GetComponent<Renderer>().material.color = F_Color;
                    ColorChangeState = 0;
                }
                //Debug.Log("디디디디버그 : " + Furn.GetComponent<Renderer>().material.color + "그리고" + F_Color);
                if (Arrow != null && Arrow.transform.parent.gameObject.name == MoveCube.name)
                {
                    Arrow.SetActive(true);
                    Arrow.layer = 0;
                }

            }
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언

            /*if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Bottom")
            {
                MoveCube.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            }
            else */
            if (Physics.Raycast(ray, out hit) && Cube_num == 0) // 레이저가 오브젝트에 맞고, 아직 선택된 것이 없을때
            {
                if (hit.collider.gameObject.tag == "Cube")//터치된것이 큐브인지 확인
                {
                    Cube_num = 1;
                    PreCube = MoveCube;
                    MoveCube = hit.collider.gameObject;
                    ChangeStatus = 0;
                    Arrow = MoveCube.transform.FindChild("RotateArrow").gameObject; // 자식오브젝트 찾기
                    if (PreCube != null)
                    {
                        PreArrow = PreCube.transform.Find("RotateArrow").gameObject;
                        PreArrow.SetActive(false);
                    }
                }
                else if (Arrow != null && hit.collider.gameObject.tag != "Arrow")
                {
                    Arrow.SetActive(false);
                }
                if (hit.collider.gameObject.name == MoveCube.transform.FindChild("RotateArrow").gameObject.name)
                {
                    //Furn = MoveCube.transform.FindChild("chair(Clone)").gameObject;
                    Arrow = MoveCube.transform.FindChild("RotateArrow").gameObject;
                    if (TouchState == 0)
                    {
                        TouchState = 1; // 누르고있는 동안 한번만 변화하기 위한 상태변수
                        Vector3 A = Arrow.transform.position;
                        MoveCube.transform.Rotate(0, 90, 0);

                        Transform MV = MoveCube.transform;
                        Arrow.transform.position = new Vector3(A.x, A.y, A.z);
                        Arrow.transform.Rotate(0, -270, 0);
                        //Transform FV = Furn.transform;

                        //Debug.Log(MV.eulerAngles.y);
                        if (MV.eulerAngles.y >= 270 && MV.eulerAngles.y <= 271)
                        {
                            Debug.Log("270!!");
                            //FV.position = new Vector3(MV.position.x-FV.localScale.x/2, MV.position.y, MV.position.z);
                        }
                        else if (MV.eulerAngles.y >= 0 && MV.eulerAngles.y <= 1)
                        {
                            Debug.Log("0!!");
                        }
                        else if (MV.eulerAngles.y >= 90 && MV.eulerAngles.y <= 91)
                        {
                            Debug.Log("90!!");
                        }
                        else if (MV.eulerAngles.y >= 180 && MV.eulerAngles.y <= 181)
                        {
                            Debug.Log("180!!");
                        }
                    }
                }
                else
                {
                    if (Arrow != null)
                        Arrow.SetActive(false);
                }
            }
            else
            {
                var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                MoveCube.transform.Translate(touchDeltaPosition.x * Time.deltaTime * 20f, 0, touchDeltaPosition.y * Time.deltaTime * 20f); //드래그
                /*
                if (hit.collider.gameObject.tag == "Bottom")
                {
                    MoveCube.transform.position = new Vector3(hit.point.x, MoveCube.transform.position.y, hit.point.z);
                }*/
            }

            /*if (Cube_num != 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                MoveCube.layer = 2;
            }
            else
            {
                MoveCube.layer = start_layer;
            }
            */
        }


        else if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > FirstDistance) // 위아래
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    MoveCube.transform.Translate(0, MoveCube.transform.position.y * Time.deltaTime, 0);
                }
                else
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    MoveCube.transform.Translate(0, -(MoveCube.transform.position.y * Time.deltaTime), 0);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
            {
                FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                FirstDistance = 0;
            }
        }
    }
}