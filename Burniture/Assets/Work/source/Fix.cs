using UnityEngine;
using System.Collections;

public class Fix : MonoBehaviour
{
    public GameObject Sphere1;
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject Sphere4;
    public GameObject Sphere5;
    public GameObject Sphere6;
    public GameObject Sphere7;
    public GameObject Sphere8;
    public GameObject Selected;
    public Vector3 v;
    /*Vector3 Sphere1_First_Position;
    Vector3 Sphere2_First_Position;
    Vector3 Sphere3_First_Position;
    Vector3 Sphere4_First_Position;
    Vector3 Sphere5_First_Position;
    Vector3 Sphere6_First_Position;
    Vector3 Sphere7_First_Position;
    Vector3 Sphere8_First_Position;*/

    void Update()
    {
        /*if (Input.touchCount == 0)
        {
            Sphere1_First_Position = Sphere1.transform.position;
            Sphere2_First_Position = Sphere2.transform.position;
            Sphere3_First_Position = Sphere3.transform.position;
            Sphere4_First_Position = Sphere4.transform.position;
            Sphere5_First_Position = Sphere5.transform.position;
            Sphere6_First_Position = Sphere6.transform.position;
            Sphere7_First_Position = Sphere7.transform.position;
            Sphere8_First_Position = Sphere8.transform.position;
        }*/
        if(Input.touchCount==1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언
            v = transform.position;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Sphere1")
                {
                    Selected = Sphere1;
                }
                else if(hit.collider.gameObject.tag == "Sphere2")
                {
                    Selected = Sphere2;
                }
                else if(hit.collider.gameObject.tag == "Sphere3")
                {
                    Selected = Sphere3;
                }
                else if(hit.collider.gameObject.tag == "Sphere4")
                {
                    Selected = Sphere4;
                }
                else if(hit.collider.gameObject.tag == "Sphere5")
                {
                    Selected = Sphere5;
                }
                else if(hit.collider.gameObject.tag == "Sphere6")
                {
                    Selected = Sphere6;
                }
                else if(hit.collider.gameObject.tag == "Sphere7")
                {
                    Selected = Sphere7;
                }
                else if(hit.collider.gameObject.tag == "Sphere8")
                {
                    Selected = Sphere8;
                }
            }
            else
            {
                if(Selected==Sphere1)
                {
                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;
                }
                else if(Selected==Sphere2)
                {
                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;
                }
                else if(Selected==Sphere3)
                {
                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;

                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v; 
                }
                else if(Selected==Sphere4)
                {
                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;
                }
                else if(Selected==Sphere5)
                {
                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;
                }
                else if(Selected==Sphere6)
                {
                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;

                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;
                }
                else if(Selected==Sphere7)
                {
                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;
                }
                else if(Selected==Sphere8)
                {
                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;

                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;
                }
            }
            /*else
            {
                //윗면 Sphere3을 기준으로
                //윗면 Sphere4
                v.x = Sphere4.transform.position.x;
                v.y = Sphere3.transform.position.y;
                v.z = Sphere3.transform.position.z;
                transform.position = v;
                Sphere4.transform.position = v;

                //윗면 Sphere5
                v.x = Sphere5.transform.position.x;
                v.y = Sphere3.transform.position.y;
                v.z = Sphere5.transform.position.z;
                transform.position = v;
                Sphere5.transform.position = v;

                //윗면 Sphere6
                v.x = Sphere3.transform.position.x;
                v.y = Sphere3.transform.position.y;
                v.z = Sphere6.transform.position.z;
                transform.position = v;
                Sphere6.transform.position = v;

                //아랫면 Sphere2를 기준으로
                //아랫면 Sphere1
                v.x = Sphere1.transform.position.x;
                v.y = Sphere2.transform.position.y;
                v.z = Sphere2.transform.position.z;
                transform.position = v;
                Sphere1.transform.position = v;

                //아랫면 Sphere7
                v.x = Sphere2.transform.position.x;
                v.y = Sphere2.transform.position.y;
                v.z = Sphere7.transform.position.z;
                transform.position = v;
                Sphere7.transform.position = v;

                //아랫면 Sphere8
                //v.x = Sphere1.transform.position.x;
                v.x = Sphere8.transform.position.x;
                v.y = Sphere2.transform.position.y;
                v.z = Sphere8.transform.position.z;
                transform.position = v;
                Sphere8.transform.position = v;*/

            /* v.x = Sphere1.transform.position.x;
             v.y = Sphere3.transform.position.y;
             v.z = Sphere3.transform.position.z;
             transform.position = v;
             Sphere4.transform.position = v;

             v.x = Sphere1.transform.position.x;
             v.y = Sphere3.transform.position.y;
             v.z = Sphere7.transform.position.z;
             transform.position = v;
             Sphere5.transform.position = v;

             v.x = Sphere3.transform.position.x;
             v.y = Sphere3.transform.position.y;
             v.z = Sphere7.transform.position.z;
             transform.position = v;
             Sphere6.transform.position = v;

             v.x = Sphere1.transform.position.x;
             v.y = Sphere1.transform.position.y;
             v.z = Sphere7.transform.position.z;
             Sphere8.transform.position = v;*/
        }
        //}
    }
}