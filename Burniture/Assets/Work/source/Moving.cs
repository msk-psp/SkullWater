using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Moving : MonoBehaviour
{

    float FirstDistance;
    public float Speed = 15f;
    public int Cube_num = 0;
    public GameObject MoveCube;
    public GameObject Furn;
    public GameObject Arrow;
    private int TouchState = 0;
    private int ChangeStatus = 1;
    public GameObject PreCube; // 이전 큐브 저장
    public GameObject PreArrow;
    
    public Material Mat;
    void Start()
    {

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
            
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Arrow != null && Arrow.transform.parent.gameObject.name == MoveCube.name)
                    Arrow.SetActive(true);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언
            
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
                if(hit.collider.gameObject.name == MoveCube.transform.FindChild("RotateArrow").gameObject.name)
                {
                    Furn = MoveCube.transform.FindChild("chair(Clone)").gameObject;
                    if (TouchState == 0)
                    {
                        TouchState = 1; // 누르고있는 동안 한번만 변화하기 위한 상태변수
                        Furn.transform.Rotate(0, 0, 90);
                        Transform FV = Furn.transform;
                        Transform MV = MoveCube.transform;
                        Debug.Log(FV.eulerAngles.y);
                        if (FV.eulerAngles.y >= 270 && FV.eulerAngles.y <= 271)
                        {
                            Debug.Log("270!!");
                            FV.position = new Vector3(MV.position.x-FV.localScale.x/2, MV.position.y, MV.position.z);
                        }
                        else if (FV.eulerAngles.y >= 0 && FV.eulerAngles.y <= 1)
                        {
                            Debug.Log("0!!");
                        }
                        else if (FV.eulerAngles.y >= 90 && FV.eulerAngles.y <= 91)
                        {
                            Debug.Log("90!!");
                        }
                        else if (FV.eulerAngles.y >= 180 && FV.eulerAngles.y <= 181)
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
            }
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