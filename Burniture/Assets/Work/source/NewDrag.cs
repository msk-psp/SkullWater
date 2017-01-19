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
                }
                else if (hit.collider.gameObject.tag == "Sphere2")
                {
                    sphere_num = 2;
                    MoveSphere = Sphere2;
                }
                else if (hit.collider.gameObject.tag == "Sphere3")
                {
                    sphere_num = 3;
                    MoveSphere = Sphere3;
                }
                else if (hit.collider.gameObject.tag == "Sphere4")
                {
                    sphere_num = 4;
                    MoveSphere = Sphere4;
                }
                else if (hit.collider.gameObject.tag == "Sphere5")
                {
                    sphere_num = 5;
                    MoveSphere = Sphere5;
                }
                else if (hit.collider.gameObject.tag == "Sphere6")
                {
                    sphere_num = 6;
                    MoveSphere = Sphere6;
                }
                else if (hit.collider.gameObject.tag == "Sphere7")
                {
                    sphere_num = 7;
                    MoveSphere = Sphere7;
                }
                else if (hit.collider.gameObject.tag == "Sphere8")
                {
                    sphere_num = 8;
                    MoveSphere = Sphere8;
                }
            }
            else
            {
                var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                MoveSphere.transform.Translate(touchDeltaPosition.x * Speed * Time.deltaTime, 0, touchDeltaPosition.y * Speed * Time.deltaTime);
            }
        }
    }
}
