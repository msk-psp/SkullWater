/***********************************
코  드 : Drag코드
수  정 : 20170324 정지훈 작성
***********************************/
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
    public GameObject Sphere1;                  // 육면체의 각 꼭지점 오브젝트
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject Sphere4;
    public GameObject Sphere5;
    public GameObject Sphere6;
    public GameObject Sphere7;
    public GameObject Sphere8;
    public GameObject MoveSphere;               // 움직일 점을 저장할 클래스
    public GameObject CeilingSphere;            // 천장의 저장을 움직일 클래스ㄴ
    public GameObject Ground;                   // 바닥의 충돌을 체크하기 위한 클래스
    public Renderer rend;                       // 투명화를 위한 객체
    public int sphere_num = 0;                  // 선택된 점의 번호
    public float FirstDistance;
    // Use this for initialization
    void Start()
    {

    }

    public void SetNewObject(GameObject[] Spheres)
    {
        Sphere1 = Spheres[0];
        Sphere2 = Spheres[1];
        Sphere3 = Spheres[2];
        Sphere4 = Spheres[3];
        Sphere5 = Spheres[4];
        Sphere6 = Spheres[5];
        Sphere7 = Spheres[6];
        Sphere8 = Spheres[7];
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)              // 터치가 없으면
        {
            sphere_num = 0;                     // sphere_num 은 0이다.
            MoveSphere = null;                  // 움직이는 Sphere는 없다.
            CeilingSphere = null;               // 움직이는 CeilingSphere는 없다.
        }
        else if (Input.touchCount == 1)              // 화면에 터치한 손가락의 갯수가 한개일때
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //debug
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit();                                  // 레이저가 맞을때를 hit라고 선언
            //int myLayerMask = 1 << 8;
            if (Physics.Raycast(ray, out hit) && sphere_num == 0) // 레이저가 오브젝트에 맞고, 아직 선택된 것이 없을때
            {
                if (hit.collider.gameObject.tag == "Sphere1")        // 해당 오브젝트가 선택시, 해당 오브젝트를 MoveSphere에 저장.
                {
                    sphere_num = 1;
                    MoveSphere = Sphere1;
                    CeilingSphere = Sphere4;
                }
                else if (hit.collider.gameObject.tag == "Sphere2")
                {
                    sphere_num = 2;
                    MoveSphere = Sphere2;
                    CeilingSphere = Sphere3;
                }
                else if (hit.collider.gameObject.tag == "Sphere7")
                {
                    sphere_num = 7;
                    MoveSphere = Sphere7;
                    CeilingSphere = Sphere6;
                }
                else if (hit.collider.gameObject.tag == "Sphere8")
                {
                    sphere_num = 8;
                    MoveSphere = Sphere8;
                    CeilingSphere = Sphere5;
                }
            }
            else if (Physics.Raycast(ray, out hit) && Input.GetTouch(0).phase == TouchPhase.Moved)          // modify section   if (ground와 충돌 상태 && Touch.GetTouch(0)의 상태)
            //else if (Physics.Raycast(ray, out hit, myLayerMask))          // modify section   if (ground와 충돌 상태 && Touch.GetTouch(0)의 상태)
            {
                MoveSphere.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                CeilingSphere.transform.position = new Vector3(hit.point.x, CeilingSphere.transform.position.y, hit.point.z);
            }
        }
        else if (Input.touchCount > 1)                                     // 높이 설정 부분.
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)     // 
            {
                if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > FirstDistance)
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    Sphere4.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                    Sphere5.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                    Sphere6.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                    Sphere3.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                }
                else
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    Sphere4.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                    Sphere5.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                    Sphere6.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                    Sphere3.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
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
