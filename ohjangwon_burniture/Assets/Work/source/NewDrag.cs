/***********************************
코  드 : 점들 선택하여 움직이는 코드
수  정 : 20170114 정지훈 작성
***********************************/

using UnityEngine;
using System.Collections;

public class NewDrag : MonoBehaviour
{
    public float Speed = 15f;
    public GameObject Sphere1;
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject Sphere4;
    public GameObject Sphere5;
    public GameObject Sphere6;
    public GameObject Sphere7;
    public GameObject Sphere8;
    public GameObject MoveSphere;               // 움직일 점을 저장할 클래스
    public int sphere_num = 0;                  // 선택된 점의 번호
    public float FirstDistance;
    //public Vector2 v1, v2;
    public Vector3 v3;
    void Start()
    {
        FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    }
    void Update()
    {
        if (Input.touchCount == 0)              // 터치가 없으면
        {
            sphere_num = 0;                     // 선택된 것도 없음
            MoveSphere = null;
        }
        if (Input.touchCount == 1)              // 화면에 터치한 손가락의 갯수가 한개일때
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언

            if (Physics.Raycast(ray, out hit) && sphere_num == 0) // 레이저가 오브젝트에 맞고, 아직 선택된 것이 없을때
            {
                //Debug.Log("collide");
                //Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Sphere1")
                {
                    sphere_num = 1;
                    MoveSphere = Sphere1;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere2")
                {
                    sphere_num = 2;
                    MoveSphere = Sphere2;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere3")
                {
                    sphere_num = 3;
                    MoveSphere = Sphere3;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere4")
                {
                    sphere_num = 4;
                    MoveSphere = Sphere4;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere5")
                {
                    sphere_num = 5;
                    MoveSphere = Sphere5;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere6")
                {
                    sphere_num = 6;
                    MoveSphere = Sphere6;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere7")
                {
                    sphere_num = 7;
                    MoveSphere = Sphere7;
                    Speed = hit.distance / 7.0f;
                }
                else if (hit.collider.gameObject.tag == "Sphere8")
                {
                    sphere_num = 8;
                    MoveSphere = Sphere8;
                    Speed = hit.distance / 7.0f;
                }
            }
            else
            {
                var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                MoveSphere.transform.Translate(touchDeltaPosition.x * Speed * Time.deltaTime, 0, touchDeltaPosition.y * Speed * Time.deltaTime);
            }
        }
        else if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //Debug.Log("움직인다.");

                //var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > FirstDistance)
                {
                    //Debug.Log("움직여");
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    Sphere3.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                    /*v3.x = Sphere4.transform.position.x;
                    v3.y = Sphere3.transform.position.y;
                    v3.z = Sphere4.transform.position.z;
                    transform.position = v3;
                    Sphere4.transform.position = v3;
                    v3.x = Sphere5.transform.position.x;
                    v3.y = Sphere3.transform.position.y;
                    v3.z = Sphere5.transform.position.z;
                    transform.position = v3;
                    Sphere5.transform.position = v3;
                    v3.x = Sphere6.transform.position.x;
                    v3.y = Sphere3.transform.position.y;
                    v3.z = Sphere6.transform.position.z;
                    transform.position = v3;
                    Sphere6.transform.position = v3;*/
                    Sphere4.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                    Sphere5.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                    Sphere6.transform.Translate(0, Sphere3.transform.position.y * Time.deltaTime, 0);
                }
                else
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    Sphere3.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                    /*v3.x = Sphere4.transform.position.x;
                    v3.y = Sphere3.transform.position.y;
                    v3.z = Sphere4.transform.position.z;
                    transform.position = v3;
                    Sphere4.transform.position = v3;
                    v3.x = Sphere5.transform.position.x;
                    v3.y = Sphere3.transform.position.y;
                    v3.z = Sphere5.transform.position.z;
                    transform.position = v3;
                    Sphere5.transform.position = v3;
                    v3.x = Sphere6.transform.position.x;
                    v3.y = Sphere3.transform.position.y;
                    v3.z = Sphere6.transform.position.z;
                    transform.position = v3;
                    Sphere6.transform.position = v3;*/
                    Sphere4.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                    Sphere5.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                    Sphere6.transform.Translate(0, -(Sphere3.transform.position.y * Time.deltaTime), 0);
                }
            }
            else if(Input.GetTouch(0).phase==TouchPhase.Began&&Input.GetTouch(1).phase==TouchPhase.Began)
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
