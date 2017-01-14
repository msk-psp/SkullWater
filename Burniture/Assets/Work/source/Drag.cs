/***********************************
코  드 : 점들 선택하여 움직이는 코드
수  정 : 20161119 오장원 수정
         20170114 오장원, 정지훈, 김민수 수정
***********************************/
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
    public float Speed = 15f;
    public GameObject Sphere1;
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject Sphere4;
    public GameObject Sphere5;
    public GameObject Sphere6;
    public GameObject Sphere7;
    public GameObject Sphere8;


    void Update()
    {
        if (Input.touchCount == 1) // 화면에 터치한 손가락의 갯수가 한개일때
        {
            //Debug.Log(Input.mousePosition);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언

            if (Physics.Raycast(ray, out hit)) // 레이저가 오브젝트에 맞았는가?
            {
                Debug.Log("collide");
                Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Sphere1" && Input.GetTouch(0).phase == TouchPhase.Moved) // 레이저와 충돌안 오브젝트의 태그가 Sphere1이고 첫번째 터치상태가 움직일때
                {
                    /* 오브젝트를 Speed의 속도로 움직임*/
                    var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere1.transform.Translate(touchDeltaPosition.x * Speed * Time.deltaTime, 0, touchDeltaPosition.y * Speed * Time.deltaTime);

                }
                else if (hit.collider.gameObject.tag == "Sphere2" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere2.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }

                else if (hit.collider.gameObject.tag == "Sphere3" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere3.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.tag == "Sphere4" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere4.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.tag == "Sphere5" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere5.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.tag == "Sphere6" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere6.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.tag == "Sphere7" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere7.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }
                else if (hit.collider.gameObject.tag == "Sphere8" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    Sphere8.transform.Translate(DeltaPosition.x * Speed * Time.deltaTime, 0, DeltaPosition.y * Speed * Time.deltaTime);
                }
            }
        }
    }
}
